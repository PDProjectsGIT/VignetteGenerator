.data

    ; Dane sta³e zczytane z programu wysokiego poziomu

    pictureWidth     dd ?   ; szerokoœæ
    centerX          dd ?   ; œrodek X
    centerY          dd ?   ; œrodek Y
    vignetteRed      dd ?   ; kolory
    vignetteGreen    dd ?
    vignetteBlue     dd ?
    maxDistance      dd ?   ; dystans winiety
    vignetteIntensity dd ?  ; intensywnoœæ

    ; Zmienne programu

    min_mask         dd 255, 255, 255, 255
    negativeOne      dd -1.0
    one              dd 1.0

    ; exp

    xarg dd ?

    result  dd ?

.code


_exp:

    sub rsp, 96

    movss dword ptr [rsp], xmm4

    finit
    fld       dword ptr [rsp] 
    fldl2e
    fmulp st(1),st(0)       ;st0 = x*log2(e) = tmp1
    fld1
    fscale              ;st0 = 2^int(tmp1), st1=tmp1
    fxch
    fld1
    fxch                ;st0 = tmp1, st1=1, st2=2^int(tmp1)
    
    fprem               ;st0 = fract(tmp1) = tmp2
    f2xm1               ;st0 = 2^(tmp2) - 1 = tmp3
    faddp st(1),st(0)       ;st0 = tmp3+1, st1 = 2^int(tmp1)
    fmulp st(1),st(0)       ;st0 = 2^int(tmp1) + 2^fract(tmp1) = 2^(x*log2(e))
    fstp      dword ptr [rsp] ; tutaj zaladuj zmienion¹ zawartosc do xmm0

    movss  xmm4, dword ptr [rsp]

    add rsp, 96
    
    ret

GenerateASM2 proc

    ; Kopiowanie do zmiennych
    mov     rdi, rcx        ; kopiuj pixelBuffer do rdi, tam bedzie uzywany
    mov     rbx, rdx        ; kopiuj start index do rbx, tam bedzie uzywany
    mov     rcx, r8         ; kopiuj end index do rcx, tam bedzie uzywany
    mov     pictureWidth, r9d
    mov     eax, [rsp + 40]
    mov     centerX, eax
    mov     eax, [rsp + 48]
    mov     centerY, eax
    mov     eax, [rsp + 56]
    mov     vignetteRed, eax
    mov     eax, [rsp + 64]
    mov     vignetteGreen, eax
    mov     eax, [rsp + 72]
    mov     vignetteBlue, eax
    mov     eax, [rsp + 80]
    mov     maxDistance, eax  
    mov     eax, [rsp + 88]
    mov     vignetteIntensity, eax  


    ; Przygotowanie pêtli g³ównej
     
    sub     rcx, rbx     ; Koniec tablicy (end index - start index)

    imul    rbx, 4              ; Pocz¹tek tablicy (razy 4 bo 4 bajty)

    movss    xmm1, min_mask      ; Przygotowanie maski to wyznaczania minimum
    shufps  xmm1, xmm1, 0       ; wype³nienie

    ; tutaj zrobiæ wektor kolorów

    xorps xmm7, xmm7

    addss  xmm7, [vignetteBlue]

    shufps xmm7, xmm7, 39h ; tutaj przesuwamy o jeden element

    addss  xmm7, [vignetteGreen]

    shufps xmm7, xmm7, 39h ; tutaj przesuwamy o jeden element

    addss xmm7, [vignetteRed]

    shufps xmm7, xmm7, 39h ; tutaj przesuwamy o jeden element
    shufps xmm7, xmm7, 39h ; tutaj przesuwamy o jeden element

    ; Pêtla przetwarzaj¹ca porcje danych

ProcessLoop:

    ; SprawdŸ, czy zosta³y jeszcze dane do przetworzenia
    cmp     rcx, 0
    jle     EndLoop

    ; Oblicz i = 4 k / (16 * pictureWidth)   = k / 4 * pictureWidth
    mov     rax,    rbx            ; dzielna 4k
    xor     rdx,    rdx            ; zerujemy rdx, reszta
    mov     r8d,    pictureWidth   ; dzielnik picture width
    imul    r8d,    16
    idiv    r8d                 

    CVTSI2SS xmm2, eax ; wynik dzielenia tutaj wczytuje j do xmm2 

    ; j = (k/4) % pictureWidth, 4k w rbx
    mov     rax,    rbx            ; dzielna 4k
    shr     rax,    4              ; przesuniecie logiczne o 4. Zatem 4k * 1/16 = k/4
    xor     rdx,    rdx            ; zerujemy rdx, reszta
    mov     r8d,    pictureWidth
    idiv    r8d

    CVTSI2SS xmm3, edx ;wynik modulo w rdx wczytuje i do xmm3

    ; Oblicz distance
    
    CVTSI2SS xmm4, centerY
    subss xmm2, xmm4 ; distanseX = i - centerY

    CVTSI2SS xmm4, centerX
    subss xmm3, xmm4 ; distanceY = j - centerX

    movdqu xmm4, xmm2
    mulps  xmm4, xmm2   ; xmm4 = distanceX ^ 2

    movdqu xmm5, xmm3
    mulps  xmm5, xmm3   ; xmm5 = distanceY ^ 2

    addss xmm4, xmm5    ; xmm4 = distanceX ^ 2 + distanceY ^ 2

    sqrtss xmm4, xmm4   ; distance = sqrt(distanceX ^ 2 + distanceY ^ 2)

    movd xmm5, maxDistance

    divss xmm4, xmm5    ; distance2 = distance/maxDistance

    movd xmm5, negativeOne

    mulps xmm4, xmm5    ; -distance2

    call _exp ;  wynik w xmm4

    movd xmm3, dword ptr [vignetteIntensity]

    mulss xmm4, xmm3 ; vignetteFactor

    ; Usuñ z kana³u alpha

    shufps xmm4, xmm4, 0

    shufps xmm4, xmm4, 93h

    subss xmm4, xmm4

    addss xmm4, [one] ; 1 dla kana³u aplha

    shufps xmm4, xmm4, 39h

    ; Wczytaj porcjê danych do xmm0
    movdqu xmm0, [rdi+rbx]

    ; przemnó¿ przez vignetteFactor
    mulps xmm0, xmm4;

    ; Ustaw kolory
    addps xmm0, xmm7
    
    ; Ustaw minimalne wartoœci dla kolorów za pomoc¹ maski
    minps xmm0, xmm1

    ; Zapisz wynik z powrotem do tablicy
    movdqu [rdi+rbx], xmm0

    ; Przesuñ indeks i zmniejsz licznik pêtli
    add     rbx, 16
    sub     rcx, 4

    ; Powrót do pocz¹tku pêtli

    jmp     ProcessLoop


EndLoop:

    ; Tutaj mo¿esz umieœciæ kod koñcz¹cy procedurê

    movdqu xmm0, xmm4
    ret
GenerateASM2 endp
end


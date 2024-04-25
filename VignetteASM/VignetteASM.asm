.data

    ; Dane sta³e zczytane z programu wysokiego poziomu

    pictureWidth     dd ?   ; szerokoœæ
    centerX          dd ?   ; œrodek X
    centerY          dd ?   ; œrodek Y
    vignetteRed      dd ?   ; kolory RGB
    vignetteGreen    dd ?
    vignetteBlue     dd ?
    maxDistance      dd ?   ; dystans winiety (od œrodka)
    vignetteIntensity dd ?  ; intensywnoœæ winiety

    ; Inne sta³e programu

    min_mask         dd 255 ; maska do wyznaczenia minimum
    negativeOne      dd -1.0
    one              dd 1.0

.code

; Procedura obliczaj¹ca exp(x)

; zmienna x w xmm4
; wynik w xmm4

_exp:

    sub rsp, 96                     ; uwzglêdnij miejsce na dane wczytane z programu wysokiego poziomu

    movss dword ptr [rsp], xmm4     ; za³aduj wartoœæ z xmm4 na stos

    finit                           ; inicjalizuj stos FPU
    fld       dword ptr [rsp]       ; za³aduj zawartoœæ ze stosu rsp
    fldl2e                          ; za³aduj log2(e)
    fmulp st(1),st(0)               ; st0 = x*log2(e) = tmp1
    fld1                            ; za³aduj 1
    fscale                          ; st0 = 2^int(tmp1), st1=tmp1
    fxch                            ; zamieñ miejscami na stosie (st0 i st1)
    fld1
    fxch                            ; st0 = tmp1, st1=1, st2=2^int(tmp1)
    fprem                           ; st0 = fract(tmp1) = tmp2
    f2xm1                           ; st0 = 2^(tmp2) - 1 = tmp3
    faddp st(1),st(0)               ; st0 = tmp3+1, st1 = 2^int(tmp1)
    fmulp st(1),st(0)               ; st0 = 2^int(tmp1) + 2^fract(tmp1) = 2^(x*log2(e))
    fstp      dword ptr [rsp]       ; wynik na stos rsp
    movss  xmm4, dword ptr [rsp]    ; wynik do xmm4

    add rsp, 96
    
    ret

GenerateASM proc

    ; Kopiowanie do zmiennych

    mov     rdi, rcx            ; kopiuj pixelBuffer do rdi, tam bedzie uzywany
    mov     rbx, rdx            ; kopiuj start index do rbx, tam bedzie uzywany
    mov     rcx, r8             ; kopiuj end index do rcx, tam bedzie uzywany
    mov     pictureWidth, r9d   ; kopiuj resztê zmiennych (sta³ych)
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
     
    sub     rcx, rbx            ; koniec tablicy (end index - start index)
    imul    rbx, 4              ; pocz¹tek tablicy (razy 4 bo 4 bajty)

    ; Wczytanie maski dla minimum

    movss    xmm1, min_mask     ; przygotowanie maski to wyznaczania minimum
    shufps  xmm1, xmm1, 0       ; wype³nienie

    ; Wczytanie wartoœci RGB do xmm7

    xorps xmm4, xmm4                ; zeruj xmm4 (tutaj bêdzie przechowywany vignette factor)

    xorps xmm7, xmm7                ; zeruj xmm7
    addss  xmm7, [vignetteBlue]
    shufps xmm7, xmm7, 39h          ; tutaj przesuwamy o jeden element
    addss  xmm7, [vignetteGreen]
    shufps xmm7, xmm7, 39h          ; tutaj przesuwamy o jeden element
    addss xmm7, [vignetteRed]
    shufps xmm7, xmm7, 39h          ; tutaj przesuwamy o jeden element
    shufps xmm7, xmm7, 39h          ; tutaj przesuwamy o jeden element (ustawienie pocz¹tkowe)

    movd xmm6, vignetteIntensity    ; za³aduj intensywnoœæ winiety do xmm6

    movd xmm8, maxDistance          ; za³aduj promieñ winiety do xmm8

    ; Pêtla przetwarzaj¹ca porcje danych

ProcessLoop:

    ; SprawdŸ, czy zosta³y jeszcze dane do przetworzenia

    cmp     rcx, 0                  ; porównaj iloœæ elementów do przetworzenia
    jle     EndLoop                 ; jeœli mniejsze b¹dŸ równe zakoñcz pêtlê

    ; Oblicz i = 4 k / (16 * pictureWidth) = k / 4 * pictureWidth

    mov     rax,    rbx             ; dzielna 4k
    xor     rdx,    rdx             ; zerujemy rdx, reszta
    mov     r8d,    pictureWidth    ; dzielnik picture width
    imul    r8d,    16
    idiv    r8d                     ; wykonaj dzielenie ze znakiem             

    CVTSI2SS xmm2, eax              ; wynik dzielenia w eax, wczytuje j do xmm2, konwersja z int na float

    ; j = (k/4) % pictureWidth, 4k w rbx
    mov     rax,    rbx             ; dzielna 4k
    shr     rax,    4               ; przesuniecie logiczne o 4. Zatem 4k * 1/16 = k/4
    xor     rdx,    rdx             ; zerujemy rdx, reszta
    mov     r8d,    pictureWidth    ; dzielnik picture width
    idiv    r8d

    CVTSI2SS xmm3, edx              ; wynik modulo w edx wczytuje i do xmm3, konwersja z int na float

    ; Oblicz distance
    
    CVTSI2SS xmm4, centerY          
    subss xmm2, xmm4                ; distanseX = i - centerY, konwersja z int na float

    CVTSI2SS xmm4, centerX
    subss xmm3, xmm4                ; distanceY = j - centerX, konwersja z int na float

    movdqu xmm4, xmm2
    mulps  xmm4, xmm2               ; xmm4 = distanceX ^ 2

    movdqu xmm5, xmm3
    mulps  xmm5, xmm3               ; xmm5 = distanceY ^ 2

    addss xmm4, xmm5                ; xmm4 = distanceX ^ 2 + distanceY ^ 2

    sqrtss xmm4, xmm4               ; distance = sqrt(distanceX ^ 2 + distanceY ^ 2)

    divss xmm4, xmm8                ; distance2 = distance/maxDistance

    movd xmm5, negativeOne          ; za³aduj -1 do xmm5

    mulps xmm4, xmm5                ; -distance2

    call _exp                       ; wykonaj exp(-distance2) wynik w xmm4

    mulss xmm4, xmm6                ; przemnó¿ vignetteFactor przez intensywnoœæ

    shufps xmm4, xmm4, 00000011b    ; Ustaw wszêdzie t¹ sam¹ wartoœæ poza ostatnim bajtem, tam zostaje 0 (kana³ alpha)

    addss xmm4, [one]               ; 1 dla kana³u aplha, przemno¿enie pozostawi bez zmian

    shufps xmm4, xmm4, 39h          ; przesuñ o jedn¹ pozycje, kana³ alpha w k+3

    ; Operacje na danych

    movdqu xmm0, [rdi+rbx]          ; wczytaj 4 bajty z tablicy do xmm0
   
    mulps xmm0, xmm4                ; przemnó¿ przez vignetteFactor

    addps xmm0, xmm7                ; dodaj wartoœci korekty kolorów
    
    minps xmm0, xmm1                ; ustaw minimalne wartoœci dla kana³ów (255) za pomoc¹ maski

    movdqu [rdi+rbx], xmm0          ; zapisz z powrotem do tablicy

    ; Przesuñ indeks i zmniejsz licznik pêtli

    add     rbx, 16                 ; przesuñ wskaŸnik o 16 (4 bajty = 4 * 4 = 16)
    sub     rcx, 4                  ; przetworzono 4 elementy

    ; Powrót do pocz¹tku pêtli

    jmp     ProcessLoop             


EndLoop:

    ret                             ; zakoñcz procedurê

GenerateASM endp
end


.data

    ; Dane sta�e zczytane z programu wysokiego poziomu

    pictureWidth     dd ?   ; szeroko��
    centerX          dd ?   ; �rodek X
    centerY          dd ?   ; �rodek Y
    vignetteRed      dd ?   ; kolory RGB
    vignetteGreen    dd ?
    vignetteBlue     dd ?
    maxDistance      dd ?   ; dystans winiety (od �rodka)
    vignetteIntensity dd ?  ; intensywno�� winiety

    ; Inne sta�e programu

    min_mask         dd 255 ; maska do wyznaczenia minimum
    negativeOne      dd -1.0
    one              dd 1.0

.code

; Procedura obliczaj�ca exp(x)

; zmienna x w xmm4
; wynik w xmm4

_exp:

    sub rsp, 96                     ; uwzgl�dnij miejsce na dane wczytane z programu wysokiego poziomu

    movss dword ptr [rsp], xmm4     ; za�aduj warto�� z xmm4 na stos

    finit                           ; inicjalizuj stos FPU
    fld       dword ptr [rsp]       ; za�aduj zawarto�� ze stosu rsp
    fldl2e                          ; za�aduj log2(e)
    fmulp st(1),st(0)               ; st0 = x*log2(e) = tmp1
    fld1                            ; za�aduj 1
    fscale                          ; st0 = 2^int(tmp1), st1=tmp1
    fxch                            ; zamie� miejscami na stosie (st0 i st1)
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
    mov     pictureWidth, r9d   ; kopiuj reszt� zmiennych (sta�ych)
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


    ; Przygotowanie p�tli g��wnej
     
    sub     rcx, rbx            ; koniec tablicy (end index - start index)
    imul    rbx, 4              ; pocz�tek tablicy (razy 4 bo 4 bajty)

    ; Wczytanie maski dla minimum

    movss    xmm1, min_mask     ; przygotowanie maski to wyznaczania minimum
    shufps  xmm1, xmm1, 0       ; wype�nienie

    ; Wczytanie warto�ci RGB do xmm7

    xorps xmm4, xmm4                ; zeruj xmm4 (tutaj b�dzie przechowywany vignette factor)

    xorps xmm7, xmm7                ; zeruj xmm7
    addss  xmm7, [vignetteBlue]
    shufps xmm7, xmm7, 39h          ; tutaj przesuwamy o jeden element
    addss  xmm7, [vignetteGreen]
    shufps xmm7, xmm7, 39h          ; tutaj przesuwamy o jeden element
    addss xmm7, [vignetteRed]
    shufps xmm7, xmm7, 39h          ; tutaj przesuwamy o jeden element
    shufps xmm7, xmm7, 39h          ; tutaj przesuwamy o jeden element (ustawienie pocz�tkowe)

    movd xmm6, vignetteIntensity    ; za�aduj intensywno�� winiety do xmm6

    movd xmm8, maxDistance          ; za�aduj promie� winiety do xmm8

    ; P�tla przetwarzaj�ca porcje danych

ProcessLoop:

    ; Sprawd�, czy zosta�y jeszcze dane do przetworzenia

    cmp     rcx, 0                  ; por�wnaj ilo�� element�w do przetworzenia
    jle     EndLoop                 ; je�li mniejsze b�d� r�wne zako�cz p�tl�

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

    movd xmm5, negativeOne          ; za�aduj -1 do xmm5

    mulps xmm4, xmm5                ; -distance2

    call _exp                       ; wykonaj exp(-distance2) wynik w xmm4

    mulss xmm4, xmm6                ; przemn� vignetteFactor przez intensywno��

    shufps xmm4, xmm4, 00000011b    ; Ustaw wsz�dzie t� sam� warto�� poza ostatnim bajtem, tam zostaje 0 (kana� alpha)

    addss xmm4, [one]               ; 1 dla kana�u aplha, przemno�enie pozostawi bez zmian

    shufps xmm4, xmm4, 39h          ; przesu� o jedn� pozycje, kana� alpha w k+3

    ; Operacje na danych

    movdqu xmm0, [rdi+rbx]          ; wczytaj 4 bajty z tablicy do xmm0
   
    mulps xmm0, xmm4                ; przemn� przez vignetteFactor

    addps xmm0, xmm7                ; dodaj warto�ci korekty kolor�w
    
    minps xmm0, xmm1                ; ustaw minimalne warto�ci dla kana��w (255) za pomoc� maski

    movdqu [rdi+rbx], xmm0          ; zapisz z powrotem do tablicy

    ; Przesu� indeks i zmniejsz licznik p�tli

    add     rbx, 16                 ; przesu� wska�nik o 16 (4 bajty = 4 * 4 = 16)
    sub     rcx, 4                  ; przetworzono 4 elementy

    ; Powr�t do pocz�tku p�tli

    jmp     ProcessLoop             


EndLoop:

    ret                             ; zako�cz procedur�

GenerateASM endp
end


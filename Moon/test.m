
test_name    dw 0
a_a dw -1
b_a dw -2
            entry
            
            addi r4,r4,48
            addi r5,r5,2
            add r6,r4,r5
            sw test_name(r0), r6
            lw r7, test_name(r0)
            putc r7
            addi r10, r0, topaddr
            sw -4(r10), r4
            sw -8(r10), r5
            lw r8, -4(r10)
            lw r9, -8(r10)
            add r11, r8, r9
            putc r11
            
            hlt
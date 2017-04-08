global
test_name    dw 55
a_a dw -1
b_a dw -2

start       entry
            
            lw r4, test_name(r0)
            putc r4
            
            addi r4,r0,48
            addi r5,r0,2
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
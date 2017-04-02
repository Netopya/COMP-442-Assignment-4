
test_name    dw 0

            entry
            
            addi r4,r4,48
            addi r5,r5,2
            add r6,r4,r5
            sw test_name(r0), r6
            lw r7, test_name(r0)
            putc r7
            hlt

global
    cake res 24
    align
program
    entry
    addi r1, r0, 49
    
    addi r2, r0, 4
    
    sw cake(r2), r1
    lw r3, cake(r2)
    
    
    putc r3
    hlt
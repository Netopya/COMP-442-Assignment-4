global
    cake res 24
    align
program
    entry
    addi r1, r0, 1
    getc r3
    add r2, r1, r3
    
    sw cake(r0), r2
    lw r3, cake(r0)
    
    
    putc r3
    hlt
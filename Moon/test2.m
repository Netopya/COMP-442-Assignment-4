global
    foo dw 48
    bar dw 5
    cake res 12
    align
program
    entry
    
    
    lw r2, foo(r0)
    sw cake(r0), r2
    
    addi r4, r0, 4
    lw r2, bar(r0)
    sw cake(r4), r2
    
    lw r2, cake(r0)
    lw r3, cake(r4)
    
    add r5, r2, r3
    
    addi r6, r0, 8
    
    sw cake(r6), r5
    
    lw r2, cake(r6)
    
    putc r2
    
    
    hlt
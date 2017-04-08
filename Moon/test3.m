global
    cake res 24
    align
program
    entry
    addi r1, r0, 2
    addi r2, r0, 1
    and r3, r1, r2
    
    bz r3, zero_15
    addi r3, r0, 1
    %sw arithmExpr_program_14(r0), r2
    j endop_15
    zero_15 addi r3, r0, 0  %sw arithmExpr_program_14(r0), r0
    endop_15
                
                
    addi r4, r0, 48
    add r5, r4, r3
    
    
    putc r5
    hlt
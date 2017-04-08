globals
                program_foo_8 dw 0
                program_bar_9 dw 0
                const_program_13 dw 48
                arithmExpr_program_14 dw 0
                const_program_15 dw 2
                arithmExpr_program_16 dw 0
                const_program_18 dw 48
                arithmExpr_program_20 dw 0
program_7
                entry
                
                getc r2
                sw program_foo_8(r0), r2
            
                
                    lw r3, program_foo_8(r0)
                    lw r4, const_program_13(r0)
                    sub r2, r3, r4
                    sw arithmExpr_program_14(r0), r2
                
                
                    lw r3, arithmExpr_program_14(r0)
                    lw r4, const_program_15(r0)
                    and r2, r3, r4
                    bz r2, zero_17
                    addi r2, r0, 1
                    sw arithmExpr_program_16(r0), r2
                    j endop_17
                    zero_17 sw arithmExpr_program_16(r0), r0
                    endop_17
                
                
                    lw r2, arithmExpr_program_16(r0)
                    sw program_bar_9(r0), r2
                
                
                    lw r3, const_program_18(r0)
                    lw r4, program_bar_9(r0)
                    add r2, r3, r4
                    sw arithmExpr_program_20(r0), r2
                
                
                lw r2, arithmExpr_program_20(r0)
                putc r2
            
                hlt

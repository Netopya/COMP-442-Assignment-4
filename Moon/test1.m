globals
                program_foo_19 dw 0
                const_program_21 dw 6
                const_program_22 dw 8
                arithmExpr_program_23 dw 0
                const_program_25 dw 3
                arithmExpr_program_26 dw 0
program_18
                entry
                
                    lw r3, const_program_22(r0)
                    lw r4, const_program_21(r0)
                    mul r2, r3, r4
                    sw arithmExpr_program_23(r0), r2
                
                
                    lw r2, arithmExpr_program_23(r0)
                    sw program_foo_19(r0), r2
                
                
                    lw r3, const_program_25(r0)
                    lw r4, program_foo_19(r0)
                    add r2, r3, r4
                    sw arithmExpr_program_26(r0), r2
                
                
                lw r2, arithmExpr_program_26(r0)
                putc r2
            
                hlt

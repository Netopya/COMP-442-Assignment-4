globals
                program_foo_20 res 8
align
                const_program_21 dw 1
                const_program_23 dw 6
                const_program_24 dw 8
                arithmExpr_program_25 dw 0
                const_program_26 dw 1
                const_program_28 dw 3
                arithmExpr_program_29 dw 0
program_19
                entry
                
                    lw r3, const_program_24(r0)
                    lw r4, const_program_23(r0)
                    mul r2, r3, r4
                    sw arithmExpr_program_25(r0), r2
                
                
                    lw r2, arithmExpr_program_25(r0)
                    sw program_foo_20(r0), r2
                
                
                    lw r3, const_program_28(r0)
                    lw r4, program_foo_20(r0)
                    add r2, r3, r4
                    sw arithmExpr_program_29(r0), r2
                
                
                lw r2, arithmExpr_program_29(r0)
                putc r2
            
                hlt

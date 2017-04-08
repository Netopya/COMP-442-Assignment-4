globals
                program_foo_8 dw 0
                const_program_11 dw 2
                arithmExpr_program_12 dw 0
program_7
                entry
                
                getc r2
                sw program_foo_8(r0), r2
            
                
                    lw r3, const_program_11(r0)
                    lw r4, program_foo_8(r0)
                    add r2, r3, r4
                    sw arithmExpr_program_12(r0), r2
                
                
                lw r2, arithmExpr_program_12(r0)
                putc r2
            
                hlt

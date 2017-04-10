globals
                program_input_114 dw 0
                program_zero_115 dw 0
                program_space_116 dw 0
                const_program_118 dw 48
                const_program_120 dw 32
                const_program_123 dw 1
                arithmExpr_program_124 dw 0
                arithmExpr_program_129 dw 0
                program_result_130 dw 0
                const_program_133 dw 5
                arithmExpr_program_134 dw 0
                arithmExpr_program_137 dw 0
                const_program_141 dw 5
                arithmExpr_program_142 dw 0
                const_program_144 dw 48
                arithmExpr_program_145 dw 0
                const_program_149 dw 5
                arithmExpr_program_150 dw 0
                arithmExpr_program_153 dw 0
program_113
                entry
                
                    lw r2, const_program_118(r0)
                    sw program_zero_115(r0), r2
                
                
                    lw r2, const_program_120(r0)
                    sw program_space_116(r0), r2
                
                
                getc r2
                sw program_input_114(r0), r2
            
                
                    lw r3, program_input_114(r0)
                    lw r4, const_program_123(r0)
                    add r2, r3, r4
                    sw arithmExpr_program_124(r0), r2
                
                
                lw r2, arithmExpr_program_124(r0)
                putc r2
            
                
                lw r2, program_space_116(r0)
                putc r2
            
                
                    lw r3, program_input_114(r0)
                    lw r4, program_zero_115(r0)
                    sub r2, r3, r4
                    sw arithmExpr_program_129(r0), r2
                
                
                    lw r2, arithmExpr_program_129(r0)
                    sw program_input_114(r0), r2
                
                
                    lw r3, program_input_114(r0)
                    lw r4, const_program_133(r0)
                    ceq r2, r3, r4
                    sw arithmExpr_program_134(r0), r2
                
                
                    lw r2, arithmExpr_program_134(r0)
                    sw program_result_130(r0), r2
                
                
                    lw r3, program_result_130(r0)
                    lw r4, program_zero_115(r0)
                    add r2, r3, r4
                    sw arithmExpr_program_137(r0), r2
                
                
                lw r2, arithmExpr_program_137(r0)
                putc r2
            
                
                lw r2, program_space_116(r0)
                putc r2
            
                
                    lw r3, program_input_114(r0)
                    lw r4, const_program_141(r0)
                    cgt r2, r3, r4
                    sw arithmExpr_program_142(r0), r2
                
                
                    lw r2, arithmExpr_program_142(r0)
                    sw program_result_130(r0), r2
                
                
                    lw r3, program_result_130(r0)
                    lw r4, const_program_144(r0)
                    add r2, r3, r4
                    sw arithmExpr_program_145(r0), r2
                
                
                lw r2, arithmExpr_program_145(r0)
                putc r2
            
                
                lw r2, program_space_116(r0)
                putc r2
            
                
                    lw r3, program_input_114(r0)
                    lw r4, const_program_149(r0)
                    clt r2, r3, r4
                    sw arithmExpr_program_150(r0), r2
                
                
                    lw r2, arithmExpr_program_150(r0)
                    sw program_result_130(r0), r2
                
                
                    lw r3, program_result_130(r0)
                    lw r4, program_zero_115(r0)
                    add r2, r3, r4
                    sw arithmExpr_program_153(r0), r2
                
                
                lw r2, arithmExpr_program_153(r0)
                putc r2
            
                hlt


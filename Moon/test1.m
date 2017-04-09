globals
                program_input_313 dw 0
                program_zero_314 dw 0
                program_space_315 dw 0
                const_program_317 dw 48
                const_program_319 dw 32
                const_program_322 dw 1
                arithmExpr_program_323 dw 0
                arithmExpr_program_328 dw 0
                program_result_329 dw 0
                const_program_332 dw 5
                arithmExpr_program_333 dw 0
                arithmExpr_program_336 dw 0
                const_program_340 dw 5
                arithmExpr_program_341 dw 0
                const_program_343 dw 48
                arithmExpr_program_344 dw 0
                const_program_348 dw 5
                arithmExpr_program_349 dw 0
                arithmExpr_program_352 dw 0
program_312
                entry
                
                    lw r2, const_program_317(r0)
                    sw program_zero_314(r0), r2
                
                
                    lw r2, const_program_319(r0)
                    sw program_space_315(r0), r2
                
                
                getc r2
                sw program_input_313(r0), r2
            
                
                    lw r3, program_input_313(r0)
                    lw r4, const_program_322(r0)
                    add r2, r3, r4
                    sw arithmExpr_program_323(r0), r2
                
                
                lw r2, arithmExpr_program_323(r0)
                putc r2
            
                
                lw r2, program_space_315(r0)
                putc r2
            
                
                    lw r3, program_input_313(r0)
                    lw r4, program_zero_314(r0)
                    sub r2, r3, r4
                    sw arithmExpr_program_328(r0), r2
                
                
                    lw r2, arithmExpr_program_328(r0)
                    sw program_input_313(r0), r2
                
                
                    lw r3, program_input_313(r0)
                    lw r4, const_program_332(r0)
                    ceq r2, r3, r4
                    sw arithmExpr_program_333(r0), r2
                
                
                    lw r2, arithmExpr_program_333(r0)
                    sw program_result_329(r0), r2
                
                
                    lw r3, program_result_329(r0)
                    lw r4, program_zero_314(r0)
                    add r2, r3, r4
                    sw arithmExpr_program_336(r0), r2
                
                
                lw r2, arithmExpr_program_336(r0)
                putc r2
            
                
                lw r2, program_space_315(r0)
                putc r2
            
                
                    lw r3, program_input_313(r0)
                    lw r4, const_program_340(r0)
                    cgt r2, r3, r4
                    sw arithmExpr_program_341(r0), r2
                
                
                    lw r2, arithmExpr_program_341(r0)
                    sw program_result_329(r0), r2
                
                
                    lw r3, program_result_329(r0)
                    lw r4, const_program_343(r0)
                    add r2, r3, r4
                    sw arithmExpr_program_344(r0), r2
                
                
                lw r2, arithmExpr_program_344(r0)
                putc r2
            
                
                lw r2, program_space_315(r0)
                putc r2
            
                
                    lw r3, program_input_313(r0)
                    lw r4, const_program_348(r0)
                    clt r2, r3, r4
                    sw arithmExpr_program_349(r0), r2
                
                
                    lw r2, arithmExpr_program_349(r0)
                    sw program_result_329(r0), r2
                
                
                    lw r3, program_result_329(r0)
                    lw r4, program_zero_314(r0)
                    add r2, r3, r4
                    sw arithmExpr_program_352(r0), r2
                
                
                lw r2, arithmExpr_program_352(r0)
                putc r2
            
                hlt

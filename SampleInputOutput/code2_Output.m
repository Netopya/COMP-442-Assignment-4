globals
                A_beer_16 dw 0
                B_okay_18 dw 0
                program_foo_25 dw 0
                program_bar_26 dw 0
                program_cake_27 dw 0
                program_baker_28 dw 0
                program_aFloat_29 dw 0
                program_reallyArray_38 res 336
align
                const_program_40 dw 3
                const_program_41 dw 1
                arithmExpr_program_42 dw 0
                const_program_46 dw 3
                const_program_47 dw 1
                arithmExpr_program_48 dw 0
program_24
                entry
                
                    lw r2, program_baker_28(r0)
                    sw program_cake_27(r0), r2
                
                
                    lw r2, A_beer_16(r0)
                    sw program_cake_27(r0), r2
                
                
                    lw r2, B_someFunc_19(r0)
                    sw program_foo_25(r0), r2
                
                
                    lw r3, const_program_40(r0)
                    lw r4, const_program_41(r0)
                    add r2, r3, r4
                    sw arithmExpr_program_42(r0), r2
                
                
                    lw r2, program_reallyArray_38(r0)
                    sw program_cake_27(r0), r2
                
                
                    lw r3, const_program_46(r0)
                    lw r4, const_program_47(r0)
                    add r2, r3, r4
                    sw arithmExpr_program_48(r0), r2
                
                
                    lw r2, program_baker_28(r0)
                    sw program_reallyArray_38(r0), r2
                
                hlt


globals
                A_beer_54 dw 0
                B_okay_56 dw 0
                program_foo_63 dw 0
                program_bar_64 dw 0
                program_cake_65 dw 0
                program_baker_66 dw 0
                program_aFloat_67 dw 0
                arithmExpr_program_71 dw 0
                arithmExpr_program_73 dw 0
                const_program_77 dw 2
                arithmExpr_program_78 dw 0
                const_program_79 dw 2
                arithmExpr_program_81 dw 0
                arithmExpr_program_82 dw 0
                arithmExpr_program_83 dw 0
                const_program_86 dw 2
                arithmExpr_program_87 dw 0
                program_reallyArray_90 res 336
align
                const_program_92 dw 4
                arithmExpr_program_94 dw 0
                const_program_95 dw 7
                arithmExpr_program_96 dw 0
                const_program_97 dw 1
                const_program_99 dw 2
                arithmExpr_program_100 dw 0
                const_program_101 dw 9
                const_program_105 dw 3
                const_program_106 dw 1
                arithmExpr_program_107 dw 0
program_62
                entry
                
                    lw r3, program_baker_66(r0)
                    lw r4, program_cake_65(r0)
                    add r2, r3, r4
                    sw arithmExpr_program_71(r0), r2
                
                
                    lw r3, arithmExpr_program_71(r0)
                    lw r4, B_anotherFunc_60(r0)
                    div r2, r3, r4
                    sw arithmExpr_program_73(r0), r2
                
                
                    lw r2, arithmExpr_program_73(r0)
                    sw program_cake_65(r0), r2
                
                
                    lw r3, program_baker_66(r0)
                    lw r4, const_program_77(r0)
                    add r2, r3, r4
                    sw arithmExpr_program_78(r0), r2
                
                
                    lw r3, const_program_79(r0)
                    lw r4, program_cake_65(r0)
                    mul r2, r3, r4
                    sw arithmExpr_program_81(r0), r2
                
                
                    lw r3, arithmExpr_program_78(r0)
                    lw r4, arithmExpr_program_81(r0)
                    sub r2, r3, r4
                    sw arithmExpr_program_82(r0), r2
                
                
                    lw r3, A_beer_54(r0)
                    lw r4, arithmExpr_program_82(r0)
                    cgt r2, r3, r4
                    sw arithmExpr_program_83(r0), r2
                
                
                    lw r2, arithmExpr_program_83(r0)
                    sw program_cake_65(r0), r2
                
                
                    lw r3, program_baker_66(r0)
                    lw r4, const_program_86(r0)
                    ceq r2, r3, r4
                    sw arithmExpr_program_87(r0), r2
                
                
                    lw r2, B_someFunc_57(r0)
                    sw program_foo_63(r0), r2
                
                
                    lw r3, const_program_92(r0)
                    lw r4, program_cake_65(r0)
                    sub r2, r3, r4
                    sw arithmExpr_program_94(r0), r2
                
                
                    lw r3, arithmExpr_program_94(r0)
                    lw r4, const_program_95(r0)
                    mul r2, r3, r4
                    sw arithmExpr_program_96(r0), r2
                
                
                    lw r3, program_baker_66(r0)
                    lw r4, const_program_99(r0)
                    mul r2, r3, r4
                    sw arithmExpr_program_100(r0), r2
                
                
                    lw r2, program_reallyArray_90(r0)
                    sw program_cake_65(r0), r2
                
                
                    lw r3, const_program_105(r0)
                    lw r4, const_program_106(r0)
                    add r2, r3, r4
                    sw arithmExpr_program_107(r0), r2
                
                
                    lw r2, program_baker_66(r0)
                    sw program_reallyArray_90(r0), r2
                
                hlt


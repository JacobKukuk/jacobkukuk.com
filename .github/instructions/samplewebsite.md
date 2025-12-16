<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>JACOB KUKUK | SYSTEM READY</title>
    <meta name="description" content="Retro Console Portfolio of Jacob Kukuk - Digital Polymath & Systems Engineer.">
    
    <!-- Tailwind CSS -->
    <script src="https://cdn.tailwindcss.com"></script>
    
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
    
    <!-- Google Fonts: Pixel Style -->
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Press+Start+2P&family=VT323&display=swap" rel="stylesheet">

    <script>
        tailwind.config = {
            theme: {
                extend: {
                    fontFamily: {
                        'retro': ['"Press Start 2P"', 'cursive'],
                        'terminal': ['"VT323"', 'monospace'],
                    },
                    colors: {
                        retro: {
                            bg: '#120b29',        // Deep purple/black
                            term: '#0c0c0c',      // Terminal black
                            neon: '#ff0099',      // Hot Pink
                            cyan: '#00f3ff',      // Cyan
                            green: '#39ff14',     // Terminal Green
                            yellow: '#fcee0a',    // Arcade Yellow
                            purple: '#b026ff',    // Deep Purple
                        }
                    },
                    animation: {
                        'blink': 'blink 1s step-end infinite',
                        'flicker': 'flicker 0.15s infinite',
                    },
                    keyframes: {
                        blink: {
                            '0%, 100%': { opacity: '1' },
                            '50%': { opacity: '0' },
                        },
                        flicker: {
                            '0%': { opacity: '0.97' },
                            '50%': { opacity: '1' },
                            '100%': { opacity: '0.98' },
                        }
                    }
                }
            }
        }
    </script>
    <style>
        /* CRT & Retro Effects */
        body {
            background-color: #050505;
            overflow-x: hidden;
            cursor: crosshair; /* Retro cursor */
        }

        /* The CRT Screen Container */
        .crt-container {
            position: relative;
            min-height: 100vh;
            background-color: #120b29;
            background-image: linear-gradient(0deg, transparent 24%, rgba(0, 255, 255, .05) 25%, rgba(0, 255, 255, .05) 26%, transparent 27%, transparent 74%, rgba(0, 255, 255, .05) 75%, rgba(0, 255, 255, .05) 76%, transparent 77%, transparent), linear-gradient(90deg, transparent 24%, rgba(0, 255, 255, .05) 25%, rgba(0, 255, 255, .05) 26%, transparent 27%, transparent 74%, rgba(0, 255, 255, .05) 75%, rgba(0, 255, 255, .05) 76%, transparent 77%, transparent);
            background-size: 50px 50px;
        }

        /* Scanlines */
        .scanlines {
            position: fixed;
            top: 0;
            left: 0;
            width: 100vw;
            height: 100vh;
            background: linear-gradient(to bottom, rgba(255,255,255,0), rgba(255,255,255,0) 50%, rgba(0,0,0,0.2) 50%, rgba(0,0,0,0.2));
            background-size: 100% 4px;
            pointer-events: none;
            z-index: 50;
        }

        /* Vignette / Screen Curvature Illusion */
        .vignette {
            position: fixed;
            top: 0;
            left: 0;
            width: 100vw;
            height: 100vh;
            background: radial-gradient(circle, rgba(0,0,0,0) 60%, rgba(0,0,0,0.6) 100%);
            pointer-events: none;
            z-index: 51;
        }

        /* Text Glow */
        .text-glow-pink { text-shadow: 2px 2px 0px #880055, 0 0 10px #ff0099; }
        .text-glow-cyan { text-shadow: 2px 2px 0px #005555, 0 0 10px #00f3ff; }
        .text-glow-green { text-shadow: 2px 2px 0px #005500, 0 0 10px #39ff14; }
        
        /* Box Styles */
        .retro-box {
            background: rgba(0, 0, 0, 0.8);
            border: 4px solid #fff;
            box-shadow: 6px 6px 0px #ff0099; /* Pink shadow */
            position: relative;
        }
        
        .retro-box::after {
            content: '';
            position: absolute;
            top: 4px;
            left: 4px;
            right: 4px;
            bottom: 4px;
            border: 2px solid #00f3ff; /* Cyan inner border */
            pointer-events: none;
        }

        /* Scrollbar */
        ::-webkit-scrollbar {
            width: 16px;
        }
        ::-webkit-scrollbar-track {
            background: #000;
        }
        ::-webkit-scrollbar-thumb {
            background: #00f3ff;
            border: 2px solid #000;
        }

        /* Boot Screen */
        #boot-screen {
            position: fixed;
            inset: 0;
            background: #000;
            z-index: 100;
            font-family: 'VT323', monospace;
            color: #39ff14;
            padding: 2rem;
            display: flex;
            flex-direction: column;
            justify-content: flex-start;
            font-size: 1.5rem;
        }
    </style>
</head>
<body class="selection:bg-retro-cyan selection:text-black">

    <!-- Audio Placeholder (No auto-play due to browser policy, but implies it) -->
    <!-- Boot Screen -->
    <div id="boot-screen">
        <div id="boot-text"></div>
    </div>

    <!-- CRT Overlay Effects -->
    <div class="scanlines"></div>
    <div class="vignette"></div>

    <div class="crt-container animate-flicker">
        
        <!-- Retro Header / HUD -->
        <header class="border-b-4 border-retro-cyan bg-black/90 p-4 sticky top-0 z-40">
            <div class="max-w-7xl mx-auto flex flex-col md:flex-row justify-between items-center gap-4">
                <div class="flex items-center gap-4">
                    <div class="w-8 h-8 bg-retro-neon animate-pulse"></div>
                    <h1 class="font-retro text-white text-lg md:text-xl tracking-widest text-glow-cyan">
                        KUKUK<span class="text-retro-neon">_OS</span> <span class="text-xs text-retro-green">v1.0</span>
                    </h1>
                </div>
                
                <nav class="font-terminal text-2xl space-x-6 text-retro-cyan">
                    <a href="#about" class="hover:text-retro-yellow hover:underline decoration-4 underline-offset-4">[BIO]</a>
                    <a href="#skills" class="hover:text-retro-yellow hover:underline decoration-4 underline-offset-4">[STATS]</a>
                    <a href="#projects" class="hover:text-retro-yellow hover:underline decoration-4 underline-offset-4">[MISSIONS]</a>
                    <a href="#contact" class="hover:text-retro-yellow hover:underline decoration-4 underline-offset-4">[CONNECT]</a>
                </nav>

                <div class="hidden md:block font-retro text-xs text-retro-green text-right">
                    <div>SCORE: <span id="score">000000</span></div>
                    <div>LOC: LAS_VEGAS</div>
                </div>
            </div>
        </header>

        <main class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-12 relative z-10">

            <!-- Hero Section: "Press Start" -->
            <section class="min-h-[80vh] flex flex-col items-center justify-center text-center mb-20 relative">
                <!-- Decorative Grid behind -->
                <div class="absolute inset-0 border-x-2 border-retro-cyan/20 pointer-events-none mx-10 md:mx-32"></div>
                <div class="absolute inset-0 border-y-2 border-retro-neon/20 pointer-events-none my-10 md:my-32"></div>

                <div class="bg-black/80 p-8 md:p-12 border-4 border-white shadow-[8px_8px_0px_#00f3ff] max-w-4xl relative">
                    <div class="absolute -top-6 left-1/2 -translate-x-1/2 bg-retro-neon text-black font-retro text-xs px-4 py-1 uppercase border-2 border-white">
                        Player 1 Selected
                    </div>

                    <h1 class="font-retro text-4xl md:text-6xl text-white mb-6 leading-relaxed">
                        JACOB<br><span class="text-retro-neon text-glow-pink">KUKUK</span>
                    </h1>

                    <div class="font-terminal text-3xl md:text-4xl text-retro-cyan mb-8">
                        &lt; DIGITAL_POLYMATH /&gt;
                    </div>

                    <p class="font-terminal text-xl md:text-2xl text-retro-green mb-10 max-w-2xl mx-auto leading-relaxed">
                        // INITIALIZING SYSTEMS...<br>
                        Systems Engineer. Full-Stack Developer. Builder of Resilient Infrastructure.
                    </p>

                    <div class="flex flex-col sm:flex-row justify-center gap-6 font-retro text-sm">
                        <a href="#projects" class="bg-retro-cyan text-black px-6 py-4 border-b-4 border-r-4 border-retro-bg hover:translate-x-[2px] hover:translate-y-[2px] hover:border-0 hover:bg-white transition-all">
                            Start Game
                        </a>
                        <a href="https://github.com/JacobKukuk" target="_blank" class="bg-transparent text-retro-cyan border-4 border-retro-cyan px-6 py-4 hover:bg-retro-cyan hover:text-black transition-colors">
                            GitHub Repo
                        </a>
                    </div>
                </div>
                
                <div class="mt-12 animate-bounce font-retro text-xs text-white">
                    INSERT COIN TO CONTINUE <span class="animate-blink">|</span>
                </div>
            </section>

            <!-- About: "Character Select" -->
            <section id="about" class="mb-32 pt-20">
                <div class="flex items-center gap-4 mb-8">
                    <i class="fa-solid fa-gamepad text-retro-neon text-4xl"></i>
                    <h2 class="font-retro text-2xl md:text-3xl text-white text-glow-pink">CHARACTER BIO</h2>
                </div>

                <div class="retro-box p-8 md:p-12 bg-retro-term text-retro-green font-terminal text-2xl leading-relaxed">
                    <p class="mb-6">
                        <span class="text-retro-neon">></span> IDENTITY: Digital Polymath<br>
                        <span class="text-retro-neon">></span> EXP: 15+ Years<br>
                        <span class="text-retro-neon">></span> CLASS: Self-Taught Engineer
                    </p>
                    <p class="mb-6 text-white">
                        I don't follow a linear path. I traverse the entire stack. From architecting custom C# integrations for the <span class="text-retro-cyan">Nevada Supreme Court</span> to physically building internet infrastructure for underserved communities.
                    </p>
                    <p class="text-retro-yellow">
                        "Specialization is for NPCs. A true Systems Engineer plays every role."
                    </p>
                </div>
            </section>

            <!-- Skills: "Stats Screen" -->
            <section id="skills" class="mb-32">
                <div class="flex items-center gap-4 mb-8">
                    <i class="fa-solid fa-microchip text-retro-cyan text-4xl"></i>
                    <h2 class="font-retro text-2xl md:text-3xl text-white text-glow-cyan">POWER LEVELS</h2>
                </div>

                <div class="grid grid-cols-1 md:grid-cols-2 gap-8">
                    <!-- Dev Stats -->
                    <div class="bg-black border-2 border-retro-purple p-6">
                        <h3 class="font-retro text-retro-purple text-lg mb-6 border-b-2 border-retro-purple pb-2">DEV_SKILLS</h3>
                        <div class="space-y-4 font-terminal text-xl text-white">
                            <div>
                                <div class="flex justify-between mb-1"><span>C# / .NET</span><span>95%</span></div>
                                <div class="h-4 bg-gray-800 border border-gray-600"><div class="h-full bg-retro-purple w-[95%] shadow-[0_0_10px_#b026ff]"></div></div>
                            </div>
                            <div>
                                <div class="flex justify-between mb-1"><span>JavaScript/ES6</span><span>90%</span></div>
                                <div class="h-4 bg-gray-800 border border-gray-600"><div class="h-full bg-retro-purple w-[90%] shadow-[0_0_10px_#b026ff]"></div></div>
                            </div>
                            <div>
                                <div class="flex justify-between mb-1"><span>SQL / T-SQL</span><span>88%</span></div>
                                <div class="h-4 bg-gray-800 border border-gray-600"><div class="h-full bg-retro-purple w-[88%] shadow-[0_0_10px_#b026ff]"></div></div>
                            </div>
                            <div>
                                <div class="flex justify-between mb-1"><span>Python</span><span>80%</span></div>
                                <div class="h-4 bg-gray-800 border border-gray-600"><div class="h-full bg-retro-purple w-[80%] shadow-[0_0_10px_#b026ff]"></div></div>
                            </div>
                        </div>
                    </div>

                    <!-- Ops Stats -->
                    <div class="bg-black border-2 border-retro-green p-6">
                        <h3 class="font-retro text-retro-green text-lg mb-6 border-b-2 border-retro-green pb-2">SYS_OPS</h3>
                        <div class="space-y-4 font-terminal text-xl text-white">
                            <div>
                                <div class="flex justify-between mb-1"><span>Azure / AWS</span><span>90%</span></div>
                                <div class="h-4 bg-gray-800 border border-gray-600"><div class="h-full bg-retro-green w-[90%] shadow-[0_0_10px_#39ff14]"></div></div>
                            </div>
                            <div>
                                <div class="flex justify-between mb-1"><span>Network/BGP</span><span>85%</span></div>
                                <div class="h-4 bg-gray-800 border border-gray-600"><div class="h-full bg-retro-green w-[85%] shadow-[0_0_10px_#39ff14]"></div></div>
                            </div>
                            <div>
                                <div class="flex justify-between mb-1"><span>Linux Admin</span><span>92%</span></div>
                                <div class="h-4 bg-gray-800 border border-gray-600"><div class="h-full bg-retro-green w-[92%] shadow-[0_0_10px_#39ff14]"></div></div>
                            </div>
                            <div>
                                <div class="flex justify-between mb-1"><span>Virtualization</span><span>88%</span></div>
                                <div class="h-4 bg-gray-800 border border-gray-600"><div class="h-full bg-retro-green w-[88%] shadow-[0_0_10px_#39ff14]"></div></div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>

            <!-- Projects: "Mission Select" -->
            <section id="projects" class="mb-32">
                <div class="flex items-center gap-4 mb-8">
                    <i class="fa-solid fa-floppy-disk text-retro-yellow text-4xl"></i>
                    <h2 class="font-retro text-2xl md:text-3xl text-white text-glow-cyan">MISSION SELECT</h2>
                </div>

                <div class="grid md:grid-cols-2 lg:grid-cols-3 gap-8">
                    
                    <!-- Project 1 -->
                    <div class="group bg-black border-4 border-gray-700 hover:border-retro-yellow transition-all duration-100 hover:-translate-y-2">
                        <div class="bg-gray-800 h-32 flex items-center justify-center border-b-4 border-gray-700 group-hover:bg-retro-yellow group-hover:border-retro-yellow transition-colors">
                            <i class="fa-solid fa-tower-broadcast text-6xl text-gray-600 group-hover:text-black"></i>
                        </div>
                        <div class="p-6">
                            <h3 class="font-retro text-white text-sm mb-2 group-hover:text-retro-yellow">VONet_CORE_DATA</h3>
                            <p class="font-terminal text-xl text-gray-400 mb-4 line-clamp-3">
                                Central nervous system for Vegas Open Network. Handles ISP user logistics and mesh routing data.
                            </p>
                            <div class="flex gap-2 mb-4">
                                <span class="bg-retro-bg text-retro-cyan px-2 font-retro text-[10px] border border-retro-cyan">C#</span>
                                <span class="bg-retro-bg text-retro-cyan px-2 font-retro text-[10px] border border-retro-cyan">SQL</span>
                            </div>
                            <a href="https://github.com/Vegas-Open-Network" target="_blank" class="block w-full text-center bg-gray-700 text-white font-retro text-xs py-3 hover:bg-retro-yellow hover:text-black transition-colors">
                                LOAD CARTRIDGE
                            </a>
                        </div>
                    </div>

                    <!-- Project 2 -->
                    <div class="group bg-black border-4 border-gray-700 hover:border-retro-neon transition-all duration-100 hover:-translate-y-2">
                        <div class="bg-gray-800 h-32 flex items-center justify-center border-b-4 border-gray-700 group-hover:bg-retro-neon group-hover:border-retro-neon transition-colors">
                            <i class="fa-solid fa-coins text-6xl text-gray-600 group-hover:text-black"></i>
                        </div>
                        <div class="p-6">
                            <h3 class="font-retro text-white text-sm mb-2 group-hover:text-retro-neon">CASINO_DEMO.EXE</h3>
                            <p class="font-terminal text-xl text-gray-400 mb-4 line-clamp-3">
                                Slot machine reward system simulation. High-concurrency SQL transaction handling logic.
                            </p>
                            <div class="flex gap-2 mb-4">
                                <span class="bg-retro-bg text-retro-neon px-2 font-retro text-[10px] border border-retro-neon">JS</span>
                                <span class="bg-retro-bg text-retro-neon px-2 font-retro text-[10px] border border-retro-neon">DB</span>
                            </div>
                            <a href="#" class="block w-full text-center bg-gray-700 text-white font-retro text-xs py-3 hover:bg-retro-neon hover:text-black transition-colors">
                                LOAD CARTRIDGE
                            </a>
                        </div>
                    </div>

                    <!-- Project 3 -->
                    <div class="group bg-black border-4 border-gray-700 hover:border-retro-green transition-all duration-100 hover:-translate-y-2">
                        <div class="bg-gray-800 h-32 flex items-center justify-center border-b-4 border-gray-700 group-hover:bg-retro-green group-hover:border-retro-green transition-colors">
                            <i class="fa-solid fa-id-card text-6xl text-gray-600 group-hover:text-black"></i>
                        </div>
                        <div class="p-6">
                            <h3 class="font-retro text-white text-sm mb-2 group-hover:text-retro-green">KIOSK_SYSTEM</h3>
                            <p class="font-terminal text-xl text-gray-400 mb-4 line-clamp-3">
                                Enterprise visitor management with Active Directory Integration and Touch UI.
                            </p>
                            <div class="flex gap-2 mb-4">
                                <span class="bg-retro-bg text-retro-green px-2 font-retro text-[10px] border border-retro-green">WPF</span>
                                <span class="bg-retro-bg text-retro-green px-2 font-retro text-[10px] border border-retro-green">AD</span>
                            </div>
                            <a href="#" class="block w-full text-center bg-gray-700 text-white font-retro text-xs py-3 hover:bg-retro-green hover:text-black transition-colors">
                                LOAD CARTRIDGE
                            </a>
                        </div>
                    </div>

                </div>
            </section>

            <!-- Experience: "Level History" -->
            <section id="experience" class="mb-32">
                <div class="flex items-center gap-4 mb-8">
                    <i class="fa-solid fa-scroll text-retro-purple text-4xl"></i>
                    <h2 class="font-retro text-2xl md:text-3xl text-white text-glow-pink">LEVEL HISTORY</h2>
                </div>

                <div class="space-y-6">
                    <!-- Job 1 -->
                    <div class="bg-black border-l-8 border-retro-cyan p-6 relative overflow-hidden">
                        <div class="absolute right-0 top-0 text-[100px] text-gray-900 font-retro -z-10 opacity-20 rotate-12">2024</div>
                        <h3 class="font-retro text-white text-lg">DB & SHAREPOINT ADMIN</h3>
                        <div class="text-retro-cyan font-terminal text-xl mb-2">NEVADA SUPREME COURT</div>
                        <p class="font-terminal text-xl text-gray-300">
                            > Architected custom C# integration for cloud file access.<br>
                            > Secured judicial data infrastructure.
                        </p>
                    </div>

                    <!-- Job 2 -->
                    <div class="bg-black border-l-8 border-retro-purple p-6 relative overflow-hidden">
                        <div class="absolute right-0 top-0 text-[100px] text-gray-900 font-retro -z-10 opacity-20 rotate-12">2021</div>
                        <h3 class="font-retro text-white text-lg">SOFTWARE DEV & SYSADMIN</h3>
                        <div class="text-retro-purple font-terminal text-xl mb-2">COLLEGE LOAN CORP</div>
                        <p class="font-terminal text-xl text-gray-300">
                            > Built full-stack Visitor Kiosk (C#, SQL).<br>
                            > Engineered real-time dashboard visualization.
                        </p>
                    </div>

                    <!-- Job 3 -->
                    <div class="bg-black border-l-8 border-retro-green p-6 relative overflow-hidden">
                        <div class="absolute right-0 top-0 text-[100px] text-gray-900 font-retro -z-10 opacity-20 rotate-12">2018</div>
                        <h3 class="font-retro text-white text-lg">SENIOR WINDOWS ADMIN</h3>
                        <div class="text-retro-green font-terminal text-xl mb-2">WHIDBEY TELECOM</div>
                        <p class="font-terminal text-xl text-gray-300">
                            > Managed Enterprise Exchange & IIS Farms.<br>
                            > PowerShell Automation Specialist.
                        </p>
                    </div>
                </div>
            </section>

            <!-- Contact: "Join Party" -->
            <section id="contact" class="pb-24">
                <div class="border-4 border-dashed border-retro-neon p-8 text-center bg-black/50">
                    <h2 class="font-retro text-3xl text-white mb-6 animate-pulse">CONTINUE?</h2>
                    <p class="font-terminal text-2xl text-retro-cyan mb-8">
                        10... 9... 8... <br>
                        Ready to build the next level?
                    </p>
                    
                    <a href="mailto:me@jacobkukuk.com" class="inline-block bg-retro-neon text-black font-retro px-8 py-4 text-sm hover:scale-105 transition-transform shadow-[4px_4px_0px_white]">
                        YES (SEND EMAIL)
                    </a>
                    
                    <div class="mt-8 flex justify-center gap-8 text-3xl text-gray-500">
                        <a href="https://github.com/JacobKukuk" class="hover:text-white transition-colors"><i class="fa-brands fa-github"></i></a>
                        <a href="#" class="hover:text-blue-500 transition-colors"><i class="fa-brands fa-linkedin"></i></a>
                        <a href="#" class="hover:text-sky-400 transition-colors"><i class="fa-brands fa-twitter"></i></a>
                    </div>
                </div>
            </section>

            <!-- Footer -->
            <footer class="text-center font-terminal text-gray-600 text-xl pb-8">
                © 2025 JACOB KUKUK | PRESS ESC TO EXIT
            </footer>

        </main>
    </div>

    <script>
        // Boot Sequence Logic
        const bootText = [
            "INITIALIZING KUKUK_BIOS v1.0...",
            "CHECKING MEMORY... 64MB OK",
            "LOADING DRIVERS... C# READY",
            "LOADING DRIVERS... SQL READY",
            "LOADING DRIVERS... AZURE READY",
            "MOUNTING FILE SYSTEM...",
            "ESTABLISHING UPLINK TO LAS VEGAS...",
            "SYSTEM READY."
        ];

        const bootScreen = document.getElementById('boot-screen');
        const textContainer = document.getElementById('boot-text');
        
        let lineIndex = 0;

        function typeLine() {
            if (lineIndex < bootText.length) {
                const line = document.createElement('div');
                line.textContent = "> " + bootText[lineIndex];
                textContainer.appendChild(line);
                lineIndex++;
                
                // Random typing delay
                const delay = Math.random() * 300 + 100;
                setTimeout(typeLine, delay);
            } else {
                setTimeout(() => {
                    bootScreen.style.opacity = '0';
                    setTimeout(() => {
                        bootScreen.style.display = 'none';
                    }, 500);
                }, 800);
            }
        }

        // Start boot sequence on load
        window.onload = typeLine;

        // Score Counter Simulation
        const scoreEl = document.getElementById('score');
        setInterval(() => {
            let score = parseInt(scoreEl.innerText);
            score += Math.floor(Math.random() * 10);
            scoreEl.innerText = score.toString().padStart(6, '0');
        }, 2000);

    </script>
</body>
</html>
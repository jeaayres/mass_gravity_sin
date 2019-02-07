fun int ntom(string s) {
    21 => int A0;
    
    s.upper() => s;
    0 => int key;
    0 => int octave;
    0 => int dec;
    //A, B, C, D, E, F, G
    [0, 2, -9, -7, -5, -4, -2] @=> int scale[];
    
    if (s.length() > 0) {
        // A
        s.charAt(0) - 65 => key;
        
        if (s.length() > 2) {
            // #
            if (s.charAt(1) == 35) 1 => dec;
            // b
            else if (s.charAt(1) == 66) -1 => dec;
                
            s.charAt(2) - 48 => octave;
        } else  if (s.length() > 1) {
            s.charAt(1) - 48 => octave;
        }
    }
    
    return A0 + scale[key] + 12 * octave + dec;
}

fun float ntof(string s) {
    s => ntom => int midi;
    return Std.mtof(midi);
}

60.0 / 140.0 => float pulse;


SawOsc o =>  LPF f => Gain g =>  dac;
SinOsc o2 => f;

SinOsc lfo => blackhole;
2. / pulse => lfo.freq;

fun void modulate() {
    while (true) {
        (0.6 + lfo.last() * 0.5) => o.gain;
        1::samp => now;
    }
}


0.15 => g.gain;

1000 => f.freq;
0.2 => f.Q;
0.3 => o2.gain;

fun void test()
{
    PulseOsc p => NRev d => dac;
    0.1 => d.mix;
    
    ["C#3", "G#3"] @=> string notes[];

    0 => int i;

    while (true)
    {
        
        Math.random2f(0.01,0.5) => p.width;
        if (Math.random2(0,1)) "F3" => ntof => p.freq;
        else notes[(i/32)%2] => ntof => p.freq;

        
        0.15 => p.gain;
        (0.6 * (pulse / 4)) :: second => now; 

        0.0 => p.gain;
        (0.4 * (pulse / 4)) :: second => now;
        
        ++i;

    }
}


spork ~test();
spork ~modulate();

"F2" => ntof => o.freq;

0 => global int stop;
while(!stop) {
    1::second => now;
}



SinOsc s => ADSR e => dac;
e.set(1::ms, 290::ms, 0, 600::ms);


e.keyOn();
246.94 * 4 => s.freq;
150::ms => now;

329.63 * 4 => s.freq;
150::ms => now;
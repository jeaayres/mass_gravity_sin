class Keyboard extends Chubgraph {
    SndBuf wav[87];
    
    fun void load(string path) {
        ["C", "Db", "D", "Eb", "E", "F", "Gb", "G", "Ab", "A", "Bb", "B"] @=> string notes[];
        
        for (0 => int i; i < 87; ++i) {
            //start at A0
            (i+9) % 12 => int id;
            (i+9) / 12 => int octave;
            
            path + "/" + notes[id] + Std.itoa(octave) + ".wav" => wav[i].read;
            
            wav[i].samples() - 1 => wav[i].pos;
            wav[i] => outlet;
        }
    }
    
    fun void play(int midi) {
        midi - 21 => int id;
        
        if (id >= 0 && id < 87)
            0 => wav[id].pos;
    }
    
    
    fun void play(int midis[]) {
        for (0 => int i; i < midis.cap(); ++i)
            play(midis[i]);
    }
    
    fun int listen() {
        MidiIn min;
        min.open(0) => int result;
        if (!result)
            return result;
        
        while (true) {
            min => now;
            MidiMsg msg;
            while (min.recv(msg)) {
                //noteon
                if (msg.data1 >= 144 && msg.data1 <= 159 && msg.data3 != 0) {
                    play(msg.data2); //note number
                }
                //msg.data3 velocity
            }
        }
    }
}


1 => global int stop;

Keyboard kb => Gain g => dac;
0.33 => g.gain;

kb.load(me.dir() + "piano");

//https://www.youtube.com/watch?v=K-Da5wiw854

// A  E F
// 57 64 65

130 => float bpm;
60 / bpm => float quarter;
4 * quarter => float whole;

// A G F E C
[81, 79, 77, 76, 72] @=> int notes[];

fun void right_hand() {
    while (true) {
        notes[Math.random2(0, 4)] => int midi;
        if (Math.randomf() > 0.5)
            midi + 12 => midi;
        midi => kb.play;
        quarter::second => now;
    }
}

spork~ right_hand();

while (true) {
    kb.play([57-12, 64-12, 57]);
    if (Math.randomf() > 0.5)
        kb.play(57-24);
    whole::second => now;
    kb.play([57-12, 65-12, 57]);
    whole::second => now;
}
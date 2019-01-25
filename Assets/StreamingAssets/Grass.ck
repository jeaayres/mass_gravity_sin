
SndBuf mySound => dac;


// get file path
me.dir() + "wav/grass.wav" => string filename;

// tell SndBuf to read this file
filename => mySound.read;
// set gain
2 => mySound.gain;
// play sound from the beginning
0 => mySound.pos;
// advance time so we can hear it
second => now;
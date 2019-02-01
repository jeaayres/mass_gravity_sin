SndBuf mySound => dac;

global float vel;

// get file path
me.dir() + "wav/wood.wav" => string filename;

// tell SndBuf to read this file
filename => mySound.read;
// set gain
Math.sqrt(vel)/2 => mySound.gain;
// play sound from the beginning
0 => mySound.pos;
// advance time so we can hear it
second => now;
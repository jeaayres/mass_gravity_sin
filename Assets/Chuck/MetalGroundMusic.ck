
SinOsc s => dac;

.1 => s.gain;


//[ 60, 62, 63, 64, 65, 66, 67, 69, 70, 71 ] @=> int notes[];
//[ 60, 63, 65, 66, 69, 66, 65, 63, 60 ] @=> int notes[];

//blues pentatonic scale built on C_4
//[60, 63, 65, 66, 67, 70, 72] @=> int notes[];
[60, 63, 65, 67, 70, 67, 65, 63, 60] @=> int notes[];

//blues pentatonic scale built on B_4
//[71, 74, 76, 77, 78, 81, 83] @=> int notes[];
//[71, 74, 76, 78, 81, 78, 76, 74, 71] @=> int notes[];

while( true )
{
    for( int i; i < notes.cap(); i++ )
    {
        play(notes[i], Math.random2( 100, 400 ) );
    }
}

fun void play( float note, float velocity )
{
    Std.mtof( note ) => s.freq;
    velocity::ms => now;
}
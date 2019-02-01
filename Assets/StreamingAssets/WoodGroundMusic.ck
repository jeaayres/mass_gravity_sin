// sound chain
ModalBar modal => NRev reverb => dac;
// set reverb mix
.1 => reverb.mix;
// modal bar parameters
7 => modal.preset;
.9 => modal.strikePosition;
10 => modal.gain;


// spork detune() as a child shred
// note: must do this before entering into infinite loop below!
spork ~ detune();

0 => global int stop;

// infinite time loop
while( !stop )
{
	1 => modal.strike;
	250::ms => now;

	.7 => modal.strike;
	250::ms => now;

	.9 => modal.strike;
	250::ms => now;


	repeat( 4 )
	// now play four quick notes
	{
	// note! and wait
	.5 => modal.strike;
	62.5::ms => now;
	}
}

// function to vary tuning over time
fun void detune()
{
	while( !stop )
	{
	// update frequency sinusoidally
	84 + Math.sin(now/second*.25*Math.PI) * 4 => Std.mtof => modal.freq;
	// advance time (controls update rate)
	5::ms => now;
	}
}
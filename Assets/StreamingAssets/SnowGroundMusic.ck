0 => global int stop;

class Imp {
	// our Simple instrument patch
	Impulse imp => ResonZ filt => dac;

	// some default settings
	100.0 => filt.Q => filt.gain;
	1000.0 => filt.freq;

	// set freq as we would any instrument
	fun void setFreq(float freq)
	{
		freq => filt.freq;
	}

	// method to allow setting Q
	fun void setQ(float Q)
	{
		Q => filt.Q;
	}

	// method to allow setting gain
	fun void setGain(float gain)
	{
		filt.Q() * gain => imp.gain;
	}


	// play a note by firing impulse
	fun void noteOn(float volume)
	{
		volume => imp.next;
	}
}

fun void hiHat (){
	Noise o=> HPF h => ADSR e => dac;

	7000 => h.freq;
	1 => h.Q;
	while ( !stop ) {
		Math.random2f(0.4,0.6) => o.gain;

		e.set(0.005::second, Math.random2f(0.1,0.2)::second, 0.0, 0.1::second);

		e.keyOn();
		0.2::second=>now;
		e.keyOff();

		e.set(0.005::second, Math.random2f(0.01,0.05)::second, 0.0, 0.1::second);

		e.keyOn();
		0.2::second=>now;
		e.keyOff();
	}
}

// Make an instance of (declare) one of our Simples
Imp s;

spork ~hiHat();

while ( !stop ) {
	// random frequency
	Std.rand2f(900.0,1300.0) => s.setFreq;
	Std.rand2f(50.0,150.0) => s.setQ;


	// play a note and wait a bit
	1 => s.noteOn;
	0.1::second => now;
}
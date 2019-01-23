
SinOsc s => dac;

.7 => s.gain;
440 => s.freq;

now + 1::second => time dt;
while (now < dt) {
	5000 - ((now - dt) / 1::second) * 4800 => s.freq;
	1::samp => now;
}

PulseOsc p => dac;
0 => global int stop;


while (!stop)
{
Math.random2f(0.01,0.5) => p.width;
if (Math.random2(0,1))
{
84.0 => p.freq;
}
else
{
100.0 => p.freq;
}
// set random pulse width
// pick a pitch randomly
// from one of
// two different pitches
0.3 => p.gain;
0.06 :: second => now; // turn on oscillator
// hang out a bit
0.0 => p.gain;
0.04 :: second => now; // turn off oscillator
// hang out a bit
}

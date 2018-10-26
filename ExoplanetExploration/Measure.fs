module Measurement

    [<Measure>]
    type Days 

    [<Measure>]
    type JupiterMass 
    [<Measure>]
    type SunMass

    [<Measure>]
    type JupiterRadius 
    [<Measure>]
    type SunRadius

    [<Measure>]
    type Kelvin

    
    let jupiterToSunMass=
        0.0009543< JupiterMass / SunMass >

    let jupiterToSunRadius =
        0.102719< JupiterRadius / SunRadius >


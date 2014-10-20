{ stdenv, mono, fsharp }:

stdenv.mkDerivation rec {
  name = "sharper-c-${version}";
  version = "0.1.0";

  src = ./.;

  buildInputs = [ mono fsharp ];

  meta = {
    description = "A library for C#";
    license = stdenv.lib.licenses.bsd3;
  };
}

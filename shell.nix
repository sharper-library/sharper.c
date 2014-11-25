with import <nixpkgs> {};
pkgs.lib.overrideDerivation (pkgs.callPackage ./. {}) (attrs: {
  inherit mono;
  inherit fsharp;
  omnisharp = omnisharp-server-git;
  buildInputs = [ fsharp ] ++ attrs.buildInputs;
  shellHook = ''export OMNISHARP_EXE_PATH="$omnisharp/lib/OmniSharp/bin/Debug/OmniSharp.exe"'';
})


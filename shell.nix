with import <nixpkgs> {};
pkgs.lib.overrideDerivation (pkgs.callPackage ./. {}) (attrs: {
  inherit mono fsharp;
})


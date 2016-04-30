Earth Model for Unity
(c) 2016 Digital Ruby, LLC
Created by Jeff Johnson

Licensed under the MIT license

I created this Earth asset in my spare time for use in prototyping space sims, and because I needed a dynamic sphere generator of better quality than Unity's default sphere. Plus I wanted an Earth with a separate cloud layer.

You should be able to make any planet you want simply by changing the materials for the earth main object and cloud layer object.

The sphere creation script defaults to IcoSphere which looks better, especially at the poles. For other non-planet cases you may have better luck with non-ico sphere. The scripts only work in the editor. Once you hit play or build a binary, the sphere generation code is stripped out. This is for performance. You definitely want to pre-generate your spheres. Maybe even make a prefab with a shared mesh that all your planets use.

I hope you enjoy this asset. Feel free to include it in your own assets, games and source code. Please just retain the MIT license in the script file and add a credit in your readme or asset description that links back to this asset. Thank you!

- Jeff
# gaucho_fps

FPS Controller
- look around: rotate view using mouse
- movement: arrow keys
- fire weapon: x key
- crosshair: white cross at center
- use cylinder as body

Level Design
- added more paths by adding assets to a prebuilt level from the assets store

Gun
- used raycast to aim
- has muzzle effect and explosion effect at collision
- leaves bullet holes on collision(except for enemies)
- Pistol: good aim, bullet speed 10, 100 ammunition, damage 10
- Sniper: poor aim, bullet speed 20, 50 ammunition, damage 20

Enemies
- patrols until sees player
- projectiles fire ball that would cause player to splash blood if hit

UI
- player health bar and ammunition for weapon are shown in upper left corner
- enemy health bar is shown on top of the enemy once it gets hit

Collectable Items
- health pack: increases 50 health
- ammo: adds 25 bullets
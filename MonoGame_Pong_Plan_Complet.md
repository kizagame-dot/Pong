# 🎮 Plan Complet — Pong en MonoGame C#
> Référence de travail personnelle · Livre support : *Complete Guide to MonoGame C#* — Morgans Higgins

---

## 🧭 Comment utiliser ce document

- **Reviens ici avant chaque session** pour savoir où tu en es
- **Coche les cases** au fur et à mesure (`[x]`)
- Chaque étape a : théorie → code annoté → exercice de validation
- Ne passe **jamais à l'étape suivante** sans avoir validé la précédente

---

## 🗺️ Vue d'ensemble du projet

```
Étape 1 → Fondations MonoGame          (Game Loop, fenêtre, couleur de fond)
Étape 2 → Rendu de formes              (SpriteBatch, rectangles, textures)
Étape 3 → Entrées clavier              (Input, delta time, mouvement)
Étape 4 → Architecture du jeu          (GameObjects, séparation des responsabilités)
Étape 5 → Physique et collisions        (AABB, rebonds, logique de la balle)
Étape 6 → Score et UI                  (SpriteFont, HUD, états de jeu)
Étape 7 → Sons                         (SoundEffect, musique de fond)
Étape 8 → Menus et états               (State Machine, écrans)
Étape 9 → Polish                       (animations, particules simples, effets visuels)
Étape 10 → Shaders HLSL                (BasicEffect, effet de glow, post-processing)
```

---

## ✅ Progression globale

- [ ] Étape 1 — Fondations MonoGame
- [ ] Étape 2 — Rendu de formes
- [ ] Étape 3 — Entrées clavier
- [ ] Étape 4 — Architecture du jeu
- [ ] Étape 5 — Physique et collisions
- [ ] Étape 6 — Score et UI
- [ ] Étape 7 — Sons
- [ ] Étape 8 — Menus et états
- [ ] Étape 9 — Polish
- [ ] Étape 10 — Shaders HLSL

---

---

# ÉTAPE 1 — Fondations MonoGame

## 🎯 Objectif
Comprendre ce qu'est un **game loop**, créer une fenêtre qui s'ouvre, et changer la couleur de fond. Rien de plus. C'est le "Hello World" du jeu vidéo.

## 📚 Théorie

### Qu'est-ce que MonoGame ?
MonoGame est un **framework open-source** dérivé de XNA (Microsoft). Il ne fait pas grand-chose tout seul — il te donne les outils pour :
- Créer une fenêtre
- Dessiner dessus 60 fois par seconde
- Lire les inputs
- Jouer des sons

### Le Game Loop — Le cœur de tout jeu vidéo

Un jeu vidéo n'est rien d'autre qu'une boucle infinie :

```
DÉMARRAGE
    ↓
LoadContent()    ← charge les assets (images, sons...)
    ↓
┌─────────────────────────────┐
│  Update(GameTime)           │  ← logique du jeu (mouvement, collisions...)
│  Draw(GameTime)             │  ← dessin à l'écran
└─────────────────────────────┘
    ↓ (répété ~60x / seconde)
```

MonoGame appelle `Update` et `Draw` automatiquement. Ton travail : **remplir ces méthodes**.

### La classe `Game`
Tout projet MonoGame hérite de la classe `Game`. Elle te donne :

| Méthode | Quand elle est appelée | À quoi elle sert |
|---|---|---|
| `Initialize()` | 1 fois au démarrage | Init des variables, états |
| `LoadContent()` | 1 fois après Initialize | Charger images, sons |
| `Update(GameTime)` | ~60x/sec | Logique du jeu |
| `Draw(GameTime)` | ~60x/sec | Dessiner |
| `UnloadContent()` | À la fermeture | Libérer la mémoire |

### `GameTime` — Le temps dans MonoGame
`GameTime` est un objet passé à `Update` et `Draw`. Il contient :
- `gameTime.ElapsedGameTime` → temps écoulé depuis le **dernier** appel (un `TimeSpan`)
- `gameTime.TotalGameTime` → temps total depuis le démarrage

**Pourquoi c'est crucial ?** Pour que le jeu tourne à la même vitesse sur tous les ordinateurs.

```csharp
// ❌ MAUVAIS — vitesse dépend du CPU
position.X += 5; // "5 pixels par frame"

// ✅ BON — vitesse indépendante du CPU
float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
position.X += 300 * deltaTime; // "300 pixels par seconde"
```

## 💻 Code — Projet de base

```csharp
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// La classe Game1 hérite de Game (fournie par MonoGame)
// "Game" contient tout le moteur : fenêtre, boucle, etc.
public class Game1 : Game
{
    // GraphicsDeviceManager : gère la fenêtre, la résolution, le plein écran
    // C'est lui qui "parle" à ta carte graphique
    private GraphicsDeviceManager _graphics;

    // SpriteBatch : l'outil pour tout dessiner (sprites, textes, formes)
    // On le déclare ici pour l'utiliser dans Draw()
    private SpriteBatch _spriteBatch;

    public Game1()
    {
        // On crée le gestionnaire graphique en lui passant "this" (notre jeu)
        _graphics = new GraphicsDeviceManager(this);

        // Définit le dossier où MonoGame cherchera les assets (images, sons)
        Content.RootDirectory = "Content";

        // Affiche le curseur de la souris dans la fenêtre
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // Définir la taille de la fenêtre
        _graphics.PreferredBackBufferWidth = 800;   // largeur en pixels
        _graphics.PreferredBackBufferHeight = 600;  // hauteur en pixels
        _graphics.ApplyChanges();                   // OBLIGATOIRE pour appliquer

        // Toujours appeler base.Initialize() — il appelle LoadContent() ensuite
        base.Initialize();
    }

    protected override void LoadContent()
    {
        // SpriteBatch a besoin de GraphicsDevice (le vrai accès GPU)
        // GraphicsDevice est une propriété héritée de Game
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        // Pour l'instant : rien. On reviendra ici.
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        // Efface l'écran avec une couleur unie
        // Color est une struct MonoGame avec des constantes pratiques :
        // Color.Black, Color.White, Color.CornflowerBlue, etc.
        // Ou une couleur custom : new Color(30, 30, 30) pour un gris très sombre
        GraphicsDevice.Clear(new Color(15, 15, 30)); // Bleu nuit profond

        // Draw entre Begin() et End()
        _spriteBatch.Begin();
        // (rien à dessiner pour l'instant)
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
```

## ✅ Validation de l'étape 1
- [ ] Une fenêtre 800×600 s'ouvre
- [ ] L'arrière-plan est une couleur personnalisée (pas le bleu par défaut)
- [ ] Le jeu tourne sans crash
- [ ] Tu peux expliquer oralement ce que font `Update` et `Draw`

---

---

# ÉTAPE 2 — Rendu de formes

## 🎯 Objectif
Dessiner les éléments visuels du Pong : **deux raquettes** (rectangles blancs) et **une balle** (carré blanc). Comprendre `Texture2D`, `SpriteBatch`, et les rectangles.

## 📚 Théorie

### Comment MonoGame dessine-t-il ?
MonoGame est un moteur **2D basé sur les sprites**. Un sprite = une `Texture2D` (image en mémoire GPU) dessinée à une position.

Pour dessiner un rectangle coloré sans charger d'image externe, on crée une `Texture2D` d'**1×1 pixel blanc** et on l'étire.

### `Texture2D` — Une image en mémoire GPU
```csharp
// Une texture = des pixels stockés sur la carte graphique
Texture2D maTexture; // déclaration

// Créer une texture 1x1 pixel blanc (technique standard pour les formes)
Texture2D pixel = new Texture2D(GraphicsDevice, 1, 1);
pixel.SetData(new[] { Color.White }); // tableau d'un seul pixel blanc
```

### `SpriteBatch.Draw()` — La méthode de dessin
```csharp
// Signature complète :
spriteBatch.Draw(
    texture,        // Texture2D : l'image à dessiner
    destinationRectangle, // Rectangle : où et quelle taille à l'écran
    color           // Color : teinte appliquée (White = couleur originale)
);
```

### `Rectangle` en MonoGame
```csharp
// Rectangle(x, y, largeur, hauteur)
// x, y = coin supérieur gauche (ATTENTION : Y augmente vers le bas !)
Rectangle rect = new Rectangle(100, 200, 15, 80);
//                              ↑    ↑    ↑   ↑
//                              x    y    w   h
```

**Système de coordonnées MonoGame :**
```
(0,0) ──────────→ X+
  │
  │
  ↓
  Y+
```Méthode              Ce que ça fait
─────────────────────────────────────────────────────
Length()         →   longueur du vecteur (= vitesse totale)
Normalize()      →   ramène la longueur à 1 (direction pure)
Distance(a, b)   →   distance entre deux points
Dot(a, b)        →   même sens ? angle entre deux vecteurs
Lerp(a, b, t)    →   point à t% entre a et b
a + b            →   addition (déplacement)
a - b            →   soustraction (vecteur entre deux points)
a * scalaire     →   changer la vitesse sans changer la direction

## 💻 Code

```csharp
public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    // La texture 1x1 pixel blanc — sert à dessiner TOUS nos rectangles
    private Texture2D _pixel;

    // Position et taille de la raquette gauche
    // On utilise Rectangle car il stocke x, y, width, height
    private Rectangle _paddleLeft;

    // Raquette droite
    private Rectangle _paddleRight;

    // La balle
    private Rectangle _ball;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = 800;
        _graphics.PreferredBackBufferHeight = 600;
        _graphics.ApplyChanges();

        // Initialiser les positions de départ
        // Raquette gauche : x=20, y=centré, 15px large, 80px haut
        _paddleLeft  = new Rectangle(20,  260, 15, 80);

        // Raquette droite : x=765 (800 - 20 - 15), y=centré
        _paddleRight = new Rectangle(765, 260, 15, 80);

        // Balle : centrée, 12x12 pixels
        _ball = new Rectangle(394, 294, 12, 12);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Créer la texture 1x1 pixel blanc
        // new Texture2D(GraphicsDevice, largeur, hauteur)
        _pixel = new Texture2D(GraphicsDevice, 1, 1);

        // SetData<T> remplit les pixels de la texture
        // On passe un tableau de 1 couleur (1 pixel)
        _pixel.SetData(new[] { Color.White });
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(15, 15, 30));

        // Begin() démarre le batch de dessin
        // SpriteSortMode.Deferred = dessine dans l'ordre des appels Draw()
        // BlendState.AlphaBlend = gère la transparence correctement
        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

        // Ligne centrale (tirets) — plusieurs petits rectangles
        for (int y = 0; y < 600; y += 30)
        {
            // Rectangle(x, y, width, height) — centré horizontalement (400 - 2 = 398)
            _spriteBatch.Draw(_pixel, new Rectangle(398, y, 4, 18), Color.White * 0.3f);
            //                                                        ↑
            //                                      Color * float = opacité (0.3 = 30%)
        }

        // Raquette gauche — blanche
        _spriteBatch.Draw(_pixel, _paddleLeft,  Color.White);

        // Raquette droite — blanche
        _spriteBatch.Draw(_pixel, _paddleRight, Color.White);

        // Balle — légèrement jaune pour la distinguer
        _spriteBatch.Draw(_pixel, _ball, Color.Yellow);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
```

## 💡 Bonnes pratiques notées ici
- **Toujours** créer `_pixel` dans `LoadContent`, pas dans `Initialize` (GraphicsDevice n'est pas encore prêt dans Initialize)
- Utiliser `Color.White * 0.3f` pour la transparence — c'est plus simple que de gérer l'alpha dans la couleur
- Les `Rectangle` stockent la position ET la taille → un seul objet à passer à `Draw()`

## ✅ Validation de l'étape 2
- [ ] Deux raquettes blanches apparaissent aux bords de l'écran
- [ ] Une balle est visible au centre
- [ ] La ligne centrale en tirets est affichée
- [ ] Tu comprends pourquoi on utilise une texture 1×1 pixel

---

---

# ÉTAPE 3 — Entrées clavier

## 🎯 Objectif
Déplacer les raquettes avec le clavier. Comprendre `KeyboardState`, le **delta time**, et pourquoi la vitesse doit être indépendante du framerate.

## 📚 Théorie

### `KeyboardState` — Snapshot du clavier
MonoGame ne te donne pas d'événements clavier (pas de "onKeyDown"). À la place, chaque `Update()` tu prends un **snapshot** de l'état actuel du clavier.

```csharp
KeyboardState kb = Keyboard.GetState();
if (kb.IsKeyDown(Keys.W)) { /* W est enfoncé EN CE MOMENT */ }
if (kb.IsKeyUp(Keys.W))   { /* W n'est PAS enfoncé */ }
```

### Détection d'une pression unique (pas maintenu)
Pour détecter "la touche vient d'être appuyée" (et non "est maintenue"), on compare l'état actuel avec l'état précédent :

```csharp
// État du frame précédent
KeyboardState _previousKeyboardState;

// Dans Update :
KeyboardState currentState = Keyboard.GetState();

// "Vient d'être appuyé" = enfoncé maintenant ET pas enfoncé avant
if (currentState.IsKeyDown(Keys.Space) && _previousKeyboardState.IsKeyUp(Keys.Space))
{
    // Action déclenchée une seule fois
}

// Stocker pour le prochain frame
_previousKeyboardState = currentState;
```

### Delta Time — La règle d'or du mouvement
```csharp
// TOUJOURS multiplier la vitesse par deltaTime
float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
// Sur un PC à 60 FPS : deltaTime ≈ 0.0167 seconde
// Sur un PC à 30 FPS : deltaTime ≈ 0.0333 seconde

// Si vitesse = 300 pixels/sec :
// À 60 FPS : 300 × 0.0167 = 5 pixels par frame
// À 30 FPS : 300 × 0.0333 = 10 pixels par frame
// Résultat : même vitesse visuelle sur les deux machines ✅
```

## 💻 Code

```csharp
public class Game1 : Game
{
    // ... (mêmes variables que l'étape 2)

    // Vitesses des raquettes en pixels par SECONDE
    private const float PaddleSpeed = 350f;

    // Position Y des raquettes en float pour plus de précision
    // (Rectangle utilise des int — on convertit à la fin)
    private float _paddleLeftY;
    private float _paddleRightY;

    protected override void Initialize()
    {
        // ... (même qu'avant)

        // Initialiser les positions Y en float
        _paddleLeftY  = 260f;
        _paddleRightY = 260f;

        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        // 1. Récupérer le delta time
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // 2. Lire le clavier
        KeyboardState kb = Keyboard.GetState();

        // --- Joueur 1 (gauche) : touches W / S ---
        if (kb.IsKeyDown(Keys.W))
        {
            _paddleLeftY -= PaddleSpeed * dt; // monter = Y diminue
        }
        if (kb.IsKeyDown(Keys.S))
        {
            _paddleLeftY += PaddleSpeed * dt; // descendre = Y augmente
        }

        // --- Joueur 2 (droite) : flèches haut / bas ---
        if (kb.IsKeyDown(Keys.Up))
        {
            _paddleRightY -= PaddleSpeed * dt;
        }
        if (kb.IsKeyDown(Keys.Down))
        {
            _paddleRightY += PaddleSpeed * dt;
        }

        // 3. Empêcher les raquettes de sortir de l'écran
        // MathHelper.Clamp(valeur, min, max) — très utile en jeu
        // min = 0 (bord du haut)
        // max = 600 - 80 = 520 (bord du bas - hauteur de la raquette)
        _paddleLeftY  = MathHelper.Clamp(_paddleLeftY,  0, 600 - 80);
        _paddleRightY = MathHelper.Clamp(_paddleRightY, 0, 600 - 80);

        // 4. Mettre à jour les Rectangle avec les nouvelles positions
        // On caste float → int car Rectangle utilise des int
        _paddleLeft  = new Rectangle(20,  (int)_paddleLeftY,  15, 80);
        _paddleRight = new Rectangle(765, (int)_paddleRightY, 15, 80);

        // Quitter avec Échap
        if (kb.IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }

    // Draw() identique à l'étape 2
}
```

## 💡 Bonnes pratiques
- Stocker les positions en `float` — les `Rectangle` utilisent des `int`, ce qui cause des pertes de précision si tu ne fais les calculs qu'avec des entiers
- `MathHelper.Clamp` est ton ami — apprends-le par cœur
- Séparer "lire l'input" / "calculer le mouvement" / "clamper" dans des blocs distincts

## ✅ Validation de l'étape 3
- [ ] W/S déplace la raquette gauche
- [ ] ↑/↓ déplace la raquette droite
- [ ] Les raquettes ne sortent pas de l'écran
- [ ] Si tu commentez `* dt`, tu vois la raquette voler à toute vitesse → tu comprends le delta time

---

---

# ÉTAPE 4 — Architecture du jeu

## 🎯 Objectif
Refactoriser le code en **classes séparées**. Éviter d'avoir tout dans `Game1`. Préparer l'architecture pour la suite.

## 📚 Théorie

### Pourquoi refactoriser maintenant ?
À ce stade, tout est dans `Game1`. Ça devient vite un "God Object" — une classe qui fait tout. En jeu vidéo, on sépare généralement :
- **Chaque entité** du jeu dans sa propre classe (`Paddle`, `Ball`)
- **La logique** de jeu dans un objet `GameManager` ou équivalent
- **Le rendu** reste dans `Draw()`

### Pattern : Classe d'entité de jeu

```csharp
public class Paddle
{
    // Données
    public Rectangle Bounds { get; private set; }
    public float Speed { get; } = 350f;
    private float _y;
    private int _x;

    // Quelle touche fait monter / descendre
    private Keys _upKey, _downKey;

    public Paddle(int x, int startY, Keys upKey, Keys downKey)
    {
        _x = x;
        _y = startY;
        _upKey = upKey;
        _downKey = downKey;
        Bounds = new Rectangle(x, startY, 15, 80);
    }

    // Update prend gameTime pour le delta time
    public void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        KeyboardState kb = Keyboard.GetState();

        if (kb.IsKeyDown(_upKey))   _y -= Speed * dt;
        if (kb.IsKeyDown(_downKey)) _y += Speed * dt;

        _y = MathHelper.Clamp(_y, 0, 600 - 80);

        // Mettre à jour le Rectangle public
        Bounds = new Rectangle(_x, (int)_y, 15, 80);
    }

    // Draw prend le spriteBatch et la texture
    public void Draw(SpriteBatch spriteBatch, Texture2D pixel)
    {
        spriteBatch.Draw(pixel, Bounds, Color.White);
    }
}
```

### `Game1` devient un chef d'orchestre
```csharp
public class Game1 : Game
{
    // ...
    private Paddle _player1;
    private Paddle _player2;
    private Ball   _ball;

    protected override void Initialize()
    {
        _player1 = new Paddle(20,  260, Keys.W, Keys.S);
        _player2 = new Paddle(765, 260, Keys.Up, Keys.Down);
        // ...
    }

    protected override void Update(GameTime gameTime)
    {
        _player1.Update(gameTime);
        _player2.Update(gameTime);
        _ball.Update(gameTime, _player1.Bounds, _player2.Bounds);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(15, 15, 30));
        _spriteBatch.Begin();
        _player1.Draw(_spriteBatch, _pixel);
        _player2.Draw(_spriteBatch, _pixel);
        _ball.Draw(_spriteBatch, _pixel);
        _spriteBatch.End();
    }
}
```

## ✅ Validation de l'étape 4
- [ ] `Game1.cs` ne contient plus de logique de mouvement
- [ ] `Paddle.cs` est une classe autonome
- [ ] `Ball.cs` est une classe autonome (même sans logique pour l'instant)
- [ ] Le jeu fonctionne exactement comme avant

---

---

# ÉTAPE 5 — Physique et collisions

## 🎯 Objectif
La balle bouge, rebondit sur les murs et les raquettes. Comprendre les **vecteurs de vitesse**, les **collisions AABB**, et les **rebonds**.

## 📚 Théorie

### Vecteurs de vitesse
La balle a une position et une **vélocité** (vitesse + direction) :

```csharp
Vector2 _position; // Position actuelle (x, y)
Vector2 _velocity; // Vitesse en pixels/sec par axe

// Ex: _velocity = new Vector2(300, -200)
// → se déplace de 300px/sec vers la droite et 200px/sec vers le haut
```

Chaque frame :
```csharp
_position += _velocity * deltaTime;
```

### Rebond sur les murs
```csharp
// Mur du haut (y < 0) ou du bas (y + hauteur > 600)
if (_position.Y < 0 || _position.Y + BallSize > 600)
{
    _velocity.Y = -_velocity.Y; // Inverser la composante Y
}
```

### Collision AABB (Axis-Aligned Bounding Box)
AABB = rectangle aligné sur les axes. C'est la collision la plus simple et la plus rapide.

```csharp
// MonoGame a Rectangle.Intersects() intégré !
Rectangle ballRect    = new Rectangle((int)_position.X, (int)_position.Y, BallSize, BallSize);
Rectangle paddleRect  = paddle.Bounds;

if (ballRect.Intersects(paddleRect))
{
    _velocity.X = -_velocity.X; // Rebond horizontal
    // Optionnel : ajouter de l'angle selon l'endroit du hit
}
```

### Angle de rebond selon le point d'impact
Pour rendre le Pong intéressant, on modifie l'angle Y selon où la balle touche la raquette :

```csharp
// Centre de la raquette vs centre de la balle
float paddleCenter = paddle.Bounds.Y + paddle.Bounds.Height / 2f;
float ballCenter   = _position.Y + BallSize / 2f;

// Différence normalisée entre -1 et 1
float relativeIntersect = (ballCenter - paddleCenter) / (paddle.Bounds.Height / 2f);

// Angle max de rebond en degrés
float bounceAngle = relativeIntersect * 60f; // ±60 degrés

// Recalculer la vitesse
float speed = _velocity.Length(); // Garder la même vitesse totale
_velocity.X = -Math.Sign(_velocity.X) * speed * (float)Math.Cos(MathHelper.ToRadians(bounceAngle));
_velocity.Y = speed * (float)Math.Sin(MathHelper.ToRadians(bounceAngle));
```

## 💻 Classe Ball

```csharp
public class Ball
{
    public const int BallSize = 12;

    private Vector2 _position;
    private Vector2 _velocity;

    // Écran
    private int _screenWidth;
    private int _screenHeight;

    public Rectangle Bounds => new Rectangle((int)_position.X, (int)_position.Y, BallSize, BallSize);

    public Ball(int screenWidth, int screenHeight)
    {
        _screenWidth  = screenWidth;
        _screenHeight = screenHeight;
        Reset();
    }

    public void Reset()
    {
        // Centrer la balle
        _position = new Vector2(_screenWidth / 2f - BallSize / 2f,
                                _screenHeight / 2f - BallSize / 2f);

        // Vitesse initiale aléatoire (angle entre -30 et +30 degrés)
        Random rng = new Random();
        float angle = (float)(rng.NextDouble() * 60 - 30); // -30 à +30
        float speed = 300f;
        int direction = rng.Next(2) == 0 ? 1 : -1; // gauche ou droite

        _velocity = new Vector2(
            direction * speed * (float)Math.Cos(MathHelper.ToRadians(angle)),
            speed * (float)Math.Sin(MathHelper.ToRadians(angle))
        );
    }

    // Retourne true si le joueur gauche marque, -1 si droite marque, 0 sinon
    public int Update(GameTime gameTime, Rectangle paddleLeft, Rectangle paddleRight)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Déplacer
        _position += _velocity * dt;

        // Rebond haut/bas
        if (_position.Y < 0)
        {
            _position.Y = 0;
            _velocity.Y = Math.Abs(_velocity.Y); // S'assurer que Y va vers le bas
        }
        if (_position.Y + BallSize > _screenHeight)
        {
            _position.Y = _screenHeight - BallSize;
            _velocity.Y = -Math.Abs(_velocity.Y); // S'assurer que Y va vers le haut
        }

        // Collision raquette gauche
        if (Bounds.Intersects(paddleLeft) && _velocity.X < 0)
        {
            _velocity.X = Math.Abs(_velocity.X); // Rebondir vers la droite
            ApplyAngle(paddleLeft);
        }

        // Collision raquette droite
        if (Bounds.Intersects(paddleRight) && _velocity.X > 0)
        {
            _velocity.X = -Math.Abs(_velocity.X); // Rebondir vers la gauche
            ApplyAngle(paddleRight);
        }

        // Balle sort à gauche → point pour le joueur droit (+1)
        if (_position.X < 0)          { Reset(); return 1; }

        // Balle sort à droite → point pour le joueur gauche (-1)
        if (_position.X > _screenWidth) { Reset(); return -1; }

        return 0; // Pas de point
    }

    private void ApplyAngle(Rectangle paddle)
    {
        float paddleCenter = paddle.Y + paddle.Height / 2f;
        float ballCenter   = _position.Y + BallSize / 2f;
        float relativeHit  = (ballCenter - paddleCenter) / (paddle.Height / 2f);
        relativeHit        = MathHelper.Clamp(relativeHit, -1f, 1f);

        float bounceAngle  = relativeHit * 55f; // ±55 degrés max
        float speed        = Math.Min(_velocity.Length() * 1.05f, 600f); // Accélération plafonnée

        _velocity.X = Math.Sign(_velocity.X) * speed * (float)Math.Cos(MathHelper.ToRadians(bounceAngle));
        _velocity.Y = speed * (float)Math.Sin(MathHelper.ToRadians(bounceAngle));
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D pixel)
    {
        spriteBatch.Draw(pixel, Bounds, Color.Yellow);
    }
}
```

## ✅ Validation de l'étape 5
- [ ] La balle se déplace dès le démarrage
- [ ] Elle rebondit sur les murs haut et bas
- [ ] Elle rebondit sur les deux raquettes
- [ ] L'angle change selon le point d'impact
- [ ] La balle se réinitialise quand elle sort de l'écran

---

---

# ÉTAPE 6 — Score et interface utilisateur

## 🎯 Objectif
Afficher le score avec `SpriteFont`, gérer les états du jeu (en cours, pause, game over).

## 📚 Théorie

### SpriteFont — Le texte dans MonoGame
Le texte dans MonoGame nécessite un fichier `.spritefont` (XML) traité par le **Content Pipeline**.

**Fichier `Score.spritefont` :**
```xml
<?xml version="1.0" encoding="utf-8"?>
<XnaContent>
  <Asset Type="Graphics:FontDescription">
    <FontName>Arial</FontName>
    <Size>48</Size>
    <Spacing>0</Spacing>
    <UseKerning>true</UseKerning>
    <Style>Bold</Style>
    <CharacterRegions>
      <CharacterRegion>
        <Start> </Start>
        <End>~</End>
      </CharacterRegion>
    </CharacterRegions>
  </Asset>
</XnaContent>
```

**Chargement et utilisation :**
```csharp
// Dans LoadContent :
SpriteFont _font = Content.Load<SpriteFont>("Score");

// Dans Draw :
string scoreText = $"{_scoreLeft}  {_scoreRight}";

// Centrer le texte
Vector2 textSize = _font.MeasureString(scoreText);
Vector2 position = new Vector2(400 - textSize.X / 2f, 20);

spriteBatch.DrawString(_font, scoreText, position, Color.White);
```

### Machine à états du jeu
Gérer les différentes phases du jeu avec un `enum` :

```csharp
public enum GameState
{
    WaitingToStart,   // Attente avant de lancer la balle
    Playing,          // Partie en cours
    Scored,           // Quelqu'un vient de marquer (pause courte)
    GameOver          // Fin de la partie
}
```

Dans `Update()` :
```csharp
switch (_gameState)
{
    case GameState.Playing:
        _player1.Update(gameTime);
        _player2.Update(gameTime);
        int result = _ball.Update(gameTime, _player1.Bounds, _player2.Bounds);
        if (result != 0) { /* mettre à jour le score, changer l'état */ }
        break;

    case GameState.WaitingToStart:
        if (Keyboard.GetState().IsKeyDown(Keys.Space))
            _gameState = GameState.Playing;
        break;
}
```

## ✅ Validation de l'étape 6
- [ ] Le score s'affiche en haut de l'écran
- [ ] Le score se met à jour quand la balle sort
- [ ] Un message "Appuyez sur Espace" s'affiche au démarrage
- [ ] Le jeu s'arrête à 7 points et affiche "Game Over"

---

---

# ÉTAPE 7 — Sons

## 🎯 Objectif
Ajouter les sons : bip quand la balle touche une raquette, son de point, musique de fond.

## 📚 Théorie

### `SoundEffect` vs `Song`
| | `SoundEffect` | `Song` |
|---|---|---|
| Utilisation | Effets courts (bips) | Musique de fond |
| Classe de lecture | `SoundEffectInstance` | `MediaPlayer` |
| Format recommandé | `.wav` | `.mp3` ou `.ogg` |

```csharp
// SoundEffect (effet court)
SoundEffect _hitSound = Content.Load<SoundEffect>("hit");
_hitSound.Play(); // Jouer une fois
// ou
SoundEffectInstance _instance = _hitSound.CreateInstance();
_instance.IsLooped = false;
_instance.Volume = 0.8f;
_instance.Play();

// Song (musique de fond)
Song _music = Content.Load<Song>("background");
MediaPlayer.IsRepeating = true;
MediaPlayer.Volume = 0.4f;
MediaPlayer.Play(_music);
```

### Générer des sons procéduraux (sans fichier audio)
Pour un Pong oldschool, on peut **générer les sons en code** :

```csharp
// Générer un bip de 440 Hz pendant 50ms
private SoundEffect GenerateBeep(float frequency, float durationSec)
{
    int sampleRate = 44100;
    int sampleCount = (int)(sampleRate * durationSec);
    short[] data = new short[sampleCount];

    for (int i = 0; i < sampleCount; i++)
    {
        // Onde sinusoïdale
        double t = (double)i / sampleRate;
        data[i] = (short)(short.MaxValue * 0.5 * Math.Sin(2 * Math.PI * frequency * t));
    }

    // Convertir short[] en byte[]
    byte[] bytes = new byte[sampleCount * 2];
    Buffer.BlockCopy(data, 0, bytes, 0, bytes.Length);

    return new SoundEffect(bytes, sampleRate, AudioChannels.Mono);
}
```

## ✅ Validation de l'étape 7
- [ ] Un son joue quand la balle touche une raquette
- [ ] Un son différent joue quand un point est marqué
- [ ] Les sons ne se chevauchent pas de façon désagréable

---

---

# ÉTAPE 8 — Menus et machine à états

## 🎯 Objectif
Créer un vrai menu principal, un écran de pause, un écran de fin de partie. Implémenter une **machine à états** propre.

## 📚 Théorie

### Screen Manager Pattern
Chaque "écran" du jeu (menu, gameplay, game over) est une classe héritant d'une interface commune :

```csharp
public abstract class Screen
{
    protected Game1 _game;

    public Screen(Game1 game) { _game = game; }

    public abstract void Update(GameTime gameTime);
    public abstract void Draw(SpriteBatch spriteBatch);
}

public class MenuScreen : Screen { /* ... */ }
public class GameScreen : Screen { /* ... */ }
public class GameOverScreen : Screen { /* ... */ }
```

```csharp
// Dans Game1 :
private Screen _currentScreen;

public void SetScreen(Screen screen)
{
    _currentScreen = screen;
}
```

## ✅ Validation de l'étape 8
- [ ] Un menu principal s'affiche au démarrage
- [ ] Appuyer sur Entrée lance la partie
- [ ] Échap met en pause
- [ ] Un écran "Game Over" s'affiche avec les scores finaux
- [ ] Depuis Game Over, on peut retourner au menu

---

---

# ÉTAPE 9 — Polish visuel

## 🎯 Objectif
Rendre le jeu beau : **effets de particules simples**, flash d'impact, trainée de la balle, animations d'interface.

## 📚 Théorie

### Système de particules simple
```csharp
public class Particle
{
    public Vector2 Position;
    public Vector2 Velocity;
    public Color Color;
    public float Life;      // Entre 0 et 1
    public float MaxLife;   // Durée de vie totale en secondes
    public float Size;

    public bool IsAlive => Life > 0;

    public void Update(float dt)
    {
        Position += Velocity * dt;
        Velocity *= 0.95f; // Friction
        Life -= dt / MaxLife;
    }
}

public class ParticleSystem
{
    private List<Particle> _particles = new List<Particle>();

    public void Emit(Vector2 position, int count, Color color)
    {
        Random rng = new Random();
        for (int i = 0; i < count; i++)
        {
            float angle  = (float)(rng.NextDouble() * Math.PI * 2);
            float speed  = (float)(rng.NextDouble() * 200 + 50);
            _particles.Add(new Particle
            {
                Position = position,
                Velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * speed,
                Color    = color,
                Life     = 1f,
                MaxLife  = 0.5f,
                Size     = (float)(rng.NextDouble() * 4 + 2)
            });
        }
    }

    public void Update(float dt)
    {
        foreach (var p in _particles) p.Update(dt);
        _particles.RemoveAll(p => !p.IsAlive);
    }

    public void Draw(SpriteBatch sb, Texture2D pixel)
    {
        foreach (var p in _particles)
        {
            // Opacité proportionnelle à la durée de vie restante
            Color c = p.Color * p.Life;
            int size = (int)(p.Size * p.Life + 1);
            sb.Draw(pixel, new Rectangle((int)p.Position.X, (int)p.Position.Y, size, size), c);
        }
    }
}
```

### Trainée de la balle (Trail)
```csharp
// Stocker les N dernières positions de la balle
Queue<Vector2> _trail = new Queue<Vector2>();
const int TrailLength = 8;

// Dans Update de Ball :
_trail.Enqueue(_position);
if (_trail.Count > TrailLength) _trail.Dequeue();

// Dans Draw :
int i = 0;
foreach (Vector2 pos in _trail)
{
    float alpha = (float)i / TrailLength * 0.5f;
    int size = (int)(BallSize * ((float)i / TrailLength));
    _spriteBatch.Draw(_pixel, new Rectangle((int)pos.X, (int)pos.Y, size, size),
                      Color.Yellow * alpha);
    i++;
}
```

## ✅ Validation de l'étape 9
- [ ] Des particules apparaissent lors des collisions
- [ ] La balle laisse une trainée
- [ ] Le score a une animation quand il change
- [ ] Le jeu est "satisfaisant" à regarder

---

---

# ÉTAPE 10 — Shaders HLSL

## 🎯 Objectif
Comprendre et écrire des **shaders HLSL**. Appliquer un effet **glow** sur la balle et les raquettes, puis un effet **scanlines** en post-processing (rendu rétro CRT).

## 📚 Théorie

### Qu'est-ce qu'un shader ?
Un shader est un **programme qui tourne sur le GPU** pour chaque pixel (ou sommet). En 2D MonoGame, on utilise principalement des **pixel shaders**.

```
CPU (ton code C#)
    → envoie les données au GPU
GPU (shader HLSL)
    → calcule la couleur finale de chaque pixel
    → affiche le résultat à l'écran
```

### Structure d'un fichier `.fx`
```hlsl
// ====== myglow.fx ======

// Les "paramètres" sont des variables envoyées depuis C#
float GlowIntensity;
float Time;

// La texture principale
texture2D MainTexture;
sampler2D MainSampler = sampler_state
{
    Texture = <MainTexture>;
};

// Structure des données vertex → pixel
struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color    : COLOR0;
    float2 TexCoord : TEXCOORD0;
};

// Le Pixel Shader : appelé pour chaque pixel
float4 PixelShaderGlow(VertexShaderOutput input) : COLOR
{
    // Échantillonner la texture à la coordonnée UV actuelle
    float4 color = tex2D(MainSampler, input.TexCoord);

    // Calculer la luminosité du pixel (0 = noir, 1 = blanc)
    float luminance = dot(color.rgb, float3(0.299, 0.587, 0.114));

    // Amplifier les zones lumineuses
    color.rgb += color.rgb * luminance * GlowIntensity;

    return color * input.Color;
}

// Technique = combinaison vertex shader + pixel shader
technique Glow
{
    pass Pass1
    {
        PixelShader = compile ps_3_0 PixelShaderGlow();
    }
}
```

### Utiliser un shader depuis C#
```csharp
// Charger le shader (fichier .fx compilé par le Content Pipeline)
Effect _glowEffect = Content.Load<Effect>("Shaders/glow");

// Définir les paramètres
_glowEffect.Parameters["GlowIntensity"].SetValue(1.5f);
_glowEffect.Parameters["Time"].SetValue((float)gameTime.TotalGameTime.TotalSeconds);

// Appliquer lors du SpriteBatch
_spriteBatch.Begin(
    SpriteSortMode.Deferred,
    BlendState.Additive,      // Additive = parfait pour le glow
    null, null, null,
    _glowEffect               // ← Le shader est appliqué ici
);
// Dessiner les éléments lumineux (balle, raquettes)
_ball.Draw(_spriteBatch, _pixel);
_spriteBatch.End();
```

### Post-processing : effet Scanlines CRT

Le post-processing = on rend le jeu dans une **RenderTarget** (texture off-screen), puis on dessine cette texture à l'écran avec un shader.

```csharp
// Déclarer un RenderTarget
RenderTarget2D _renderTarget;

// Dans LoadContent :
_renderTarget = new RenderTarget2D(GraphicsDevice, 800, 600);

// Dans Draw :
// --- Passe 1 : Rendu du jeu dans la RenderTarget ---
GraphicsDevice.SetRenderTarget(_renderTarget);
GraphicsDevice.Clear(new Color(15, 15, 30));
_spriteBatch.Begin();
// ... dessiner tout le jeu ...
_spriteBatch.End();

// --- Passe 2 : Appliquer le shader et dessiner à l'écran ---
GraphicsDevice.SetRenderTarget(null); // Retour à l'écran
GraphicsDevice.Clear(Color.Black);

_scanlineEffect.Parameters["ScreenHeight"].SetValue(600f);
_scanlineEffect.Parameters["Intensity"].SetValue(0.15f);

_spriteBatch.Begin(
    SpriteSortMode.Immediate,
    BlendState.Opaque,
    null, null, null,
    _scanlineEffect
);
_spriteBatch.Draw(_renderTarget, Vector2.Zero, Color.White);
_spriteBatch.End();
```

**Shader scanlines HLSL :**
```hlsl
// scanlines.fx
float ScreenHeight;
float Intensity; // 0 = invisible, 1 = très intense

texture2D MainTexture;
sampler2D MainSampler = sampler_state { Texture = <MainTexture>; };

float4 ScanlinePS(float2 texCoord : TEXCOORD0) : COLOR
{
    float4 color = tex2D(MainSampler, texCoord);

    // Coordonnée Y en pixels
    float scanline = texCoord.y * ScreenHeight;

    // Lignes impaires légèrement assombries
    // fmod(x, 2.0) = reste de x / 2
    float mask = 1.0 - Intensity * step(1.0, fmod(scanline, 2.0));

    return float4(color.rgb * mask, color.a);
}

technique Scanlines
{
    pass Pass1
    {
        PixelShader = compile ps_3_0 ScanlinePS();
    }
}
```

## ✅ Validation de l'étape 10
- [ ] Un shader glow est appliqué sur la balle
- [ ] L'effet scanlines est visible en post-processing
- [ ] Tu peux modifier les paramètres du shader en temps réel (ex: intensité)
- [ ] Tu comprends la différence entre vertex shader et pixel shader

---

---

# 📖 Annexes

## Correspondance avec le livre de Morgans Higgins

| Étape | Chapitres suggérés |
|---|---|
| 1 — Fondations | Ch. 1-2 : Project Setup, Game Loop |
| 2 — Rendu | Ch. 3 : SpriteBatch, Textures |
| 3 — Inputs | Ch. 4 : Input Management |
| 4 — Architecture | Ch. 5 : Game Architecture |
| 5 — Physique | Ch. 6-7 : Collisions, Physics |
| 6 — UI | Ch. 8 : UI, Fonts |
| 7 — Sons | Ch. 9 : Audio |
| 8 — États | Ch. 10 : Game States |
| 9 — Polish | Ch. 11 : Particle Systems |
| 10 — Shaders | Ch. 12-13 : Shaders, Effects |

## Cheatsheet MonoGame rapide

```csharp
// Lire clavier
KeyboardState kb = Keyboard.GetState();
kb.IsKeyDown(Keys.Space)

// Delta time
float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

// Clamp
MathHelper.Clamp(valeur, min, max)

// Angle → radians
MathHelper.ToRadians(degrés)

// Collision
rect1.Intersects(rect2) // bool

// Mesurer du texte
Vector2 size = font.MeasureString("texte");

// Couleur avec opacité
Color.White * 0.5f

// Dessiner texte
spriteBatch.DrawString(font, "texte", position, color);

// RenderTarget pour post-processing
GraphicsDevice.SetRenderTarget(renderTarget); // off-screen
GraphicsDevice.SetRenderTarget(null);         // retour à l'écran
```

## Pièges classiques à éviter

| Piège | Solution |
|---|---|
| Vitesse liée au framerate | Toujours multiplier par `deltaTime` |
| Position en int → imprécision | Stocker en `float`, convertir en `int` pour Rectangle |
| Créer une texture dans Update | Créer les ressources dans `LoadContent` uniquement |
| Oublier `base.Update()` / `base.Draw()` | Toujours appeler les méthodes de base |
| Rectangle Y=0 en haut | Le Y augmente vers le bas en MonoGame |
| Shader pas compilé dans Content Pipeline | Ajouter le `.fx` au projet MGCB et le compiler |

---

*Document créé le — Projet Pong MonoGame C#*
*À mettre à jour au fil de l'avancement du projet.*

namespace Pong.Interface;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.Screens;
interface IScreen
{
    void Update(GameTime gameTime);
    void Draw();
}


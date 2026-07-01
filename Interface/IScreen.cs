namespace Pong.Interface;

using Microsoft.Xna.Framework;
using Pong.Core;
using Pong.Screens;
interface IScreen
{
    void Update(GameTime gameTime);
    void Draw();
}


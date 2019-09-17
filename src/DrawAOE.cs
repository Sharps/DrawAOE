using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExileCore;
using ExileCore.PoEMemory;
using ExileCore.PoEMemory.Components;
using ExileCore.PoEMemory.MemoryObjects;
using ExileCore.Shared.Enums;
using SharpDX;

namespace DrawAOE
{
    public class DrawAOE : BaseSettingsPlugin<DrawAOESettings>
    {
        private bool isTown;


        public override bool Initialise()
        {

            OnSettingsToggle();


            return true;
        }

        private void OnSettingsToggle()
        {
            try
            {
                if (Settings.Enable.Value)
                {

                    GameController.Area.RefreshState();

                    isTown = GameController.Area.CurrentArea.IsTown;


                }
                else
                {
                    return;
                }
            }
            catch (Exception)
            {
            }
        }


        public override void Render()
        {
            try
            {
                if (!Settings.Enable.Value) return;


                if (Settings.CircleEnable.Value)
                {
                    if (!Settings.DisplayInTown.Value && isTown)
                    {
                        return;
                    }
                    else
                    {
                        var pos = GameController.Game.IngameState.Data.LocalPlayer.GetComponent<Render>().Pos;
                        DrawEllipseToWorld(pos, Settings.CircleSize.Value, 50, Settings.LineWidth.Value, Settings.LineColor.Value);
                    }
                }

                if (Settings.CircleEnable2.Value)
                {
                    if (!Settings.DisplayInTown.Value && isTown)
                    {
                        return;
                    }
                    else
                    {
                        var pos = GameController.Game.IngameState.Data.LocalPlayer.GetComponent<Render>().Pos;
                        DrawEllipseToWorld(pos, Settings.CircleSize2.Value, 50, Settings.LineWidth2.Value, Settings.LineColor2.Value);
                    }
                }



            }
            catch (Exception)
            {
            }


        }

        private void OnAreaChange(AreaController area)
        {
            if (Settings.Enable.Value)
            {
                isTown = area.CurrentArea.IsTown;
            }
        }


        public void DrawEllipseToWorld(Vector3 vector3Pos, int radius, int points, int lineWidth, Color color)
        {
            var camera = GameController.Game.IngameState.Camera;


            float posOffset = GameController.Game.IngameState.Data.LocalPlayer.GetComponent<Render>().Bounds.Z;

            var plottedCirclePoints = new List<Vector3>();
            var slice = 2 * Math.PI / points;
            for (var i = 0; i < points; i++)
            {
                var angle = slice * i;
                var x = (decimal)vector3Pos.X + decimal.Multiply((decimal)radius, (decimal)Math.Cos(angle));
                var y = (decimal)vector3Pos.Y + decimal.Multiply((decimal)radius, (decimal)Math.Sin(angle));
                plottedCirclePoints.Add(new Vector3((float)x, (float)y, vector3Pos.Z + posOffset - 20));
            }


            var rndEntity = GameController.Entities.FirstOrDefault(x =>
                x.HasComponent<Render>() && GameController.Player.Address != x.Address);

            for (var i = 0; i < plottedCirclePoints.Count; i++)
            {
                if (i >= plottedCirclePoints.Count - 1)
                {
                    var pointEnd1 = camera.WorldToScreen(plottedCirclePoints.Last());
                    var pointEnd2 = camera.WorldToScreen(plottedCirclePoints[0]);
                    Graphics.DrawLine(pointEnd1, pointEnd2, lineWidth, color);
                    return;
                }

                var point1 = camera.WorldToScreen(plottedCirclePoints[i]);
                var point2 = camera.WorldToScreen(plottedCirclePoints[i + 1]);
                Graphics.DrawLine(point1, point2, lineWidth, color);
            }
        }
    }
}

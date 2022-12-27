using UnityEngine;

namespace Blocks
{
    public class IceBlock : SpecialBlock
    {
        private bool _first = true;
        protected override void SetPattern(Color color)
        {
            if (color == Color.white)
            {
                color = PuzzleGame.RandomColor;
            }

            Pattern = color;
            SetSpecialIcon("Textures/icon_ice", Color.white);
        }

        public override void OnRoundEnd()
        {
            if (_first)
            {
                _first = false;
                return;
            }
            if (Slot.LeftSlot && Slot.LeftSlot.SubBlock is NormalBlock b1)
            {
                ChangeToIceBlock(b1);
            }
            
            if (Slot.RightSlot && Slot.RightSlot.SubBlock is NormalBlock b2)
            {
                ChangeToIceBlock(b2);
            }
            
            if (Slot.UpSlot && Slot.UpSlot.SubBlock is NormalBlock b3)
            {
                ChangeToIceBlock(b3);
            }
            
            if (Slot.DownSlot && Slot.DownSlot.SubBlock is NormalBlock b4)
            {
                ChangeToIceBlock(b4);
            }
        }

        private void ChangeToIceBlock(NormalBlock block)
        {
            var slot = block.Slot;
            var pattern = block.Pattern;
            slot.RemoveMainBlock(false);
            slot.GenerateBlock(GetType(), pattern);
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Blocks
{
    public class IceBlock : SpecialBlock
    {
        private List<NormalBlock> _beEffectBlocks = new();
        protected override void SetPattern(Color color)
        {
            if (color == Color.white)
            {
                color = PuzzleGame.RandomColor;
            }

            Pattern = color;
            SetSpecialIcon("Textures/icon_radiation", Color.white);
        }

        public override bool CheckExecuteEffect()
        {
            var result = false;
            if (Slot.LeftSlot && Slot.LeftSlot.SubBlock is NormalBlock b1)
            {
                result = true;
                _beEffectBlocks.Add(b1);
            }
            
            if (Slot.RightSlot && Slot.RightSlot.SubBlock is NormalBlock b2)
            {
                result = true;
                _beEffectBlocks.Add(b2);
            }
            
            if (Slot.UpSlot && Slot.UpSlot.SubBlock is NormalBlock b3)
            {
                result = true;
                _beEffectBlocks.Add(b3);
            }
            
            if (Slot.DownSlot && Slot.DownSlot.SubBlock is NormalBlock b4)
            {
                result = true;
                _beEffectBlocks.Add(b4);
            }

            return result;
        }

        public override void ExecuteEffect()
        {
            foreach (var block in _beEffectBlocks)
            {
                ChangeToIceBlock(block);
            }
            _beEffectBlocks.Clear();
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
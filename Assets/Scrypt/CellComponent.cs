using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Checkers
{
    public class CellComponent : BaseClickComponent
    {
        public bool IsFreeCellToMove { get; set; }

        [SerializeField] private Material _selectMaterial;
        [SerializeField] private Material _freeCellMaterial;

        private Dictionary<NeighborType, CellComponent> _neighbors;

        /// <summary>
        /// ���������� ������ ������ �� ���������� �����������
        /// </summary>
        /// <param name="type">������������ �����������</param>
        /// <returns>������-����� ��� null</returns>
        public CellComponent GetNeighbors(NeighborType type) => _neighbors[type];

        public void HighLightFreeCellToMove()
        {
            SetMaterial(_freeCellMaterial);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (EventSystem.current == null)
            {
                SetMaterial();
                return;
            }

            SetMaterial(_selectMaterial);
            CallBackEvent(this, true);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            if (IsFreeCellToMove)
            {
                SetMaterial(_freeCellMaterial);
            }
            else
            {
                SetMaterial();
            }

            CallBackEvent(this, false);
        }

        public override IEnumerator Move(BaseClickComponent cell)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// ���������������� ������ ������
        /// </summary>
        public void Configuration(Dictionary<NeighborType, CellComponent> neighbors)
        {
            if (_neighbors != null) return;
            _neighbors = neighbors;
        }

    }

    /// <summary>
    /// ��� ������ ������
    /// </summary>
    public enum NeighborType : byte
    {
        /// <summary>
        /// ������ ������ � ����� �� ������
        /// </summary>
        TopLeft,
        /// <summary>
        /// ������ ������ � ������ �� ������
        /// </summary>
        TopRight,
        /// <summary>
        /// ������ ����� � ����� �� ������
        /// </summary>
        BottomLeft,
        /// <summary>
        /// ������ ����� � ������ �� ������
        /// </summary>
        BottomRight
    }
}
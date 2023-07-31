using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Checkers
{
    public abstract class BaseClickComponent : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [field: SerializeField]
        public Material WhiteMaterial { get; set; }

        [field: SerializeField]
        public Material BlackMaterial { get; set; }

        public Coordinate Coordinate;

        private Material _defaultMaterial;

        private MeshRenderer _meshRenderer;

        public ColorType Color { get; set; }
        public BaseClickComponent Pair { get; set; }

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        public void SetDefaultMaterial(Material material)
        {
            _defaultMaterial = material;
            SetMaterial(material);
        }

        public void SetMaterial(Material material = null)
        {
            _meshRenderer.sharedMaterial = material ? material : _defaultMaterial;
        }

        /// <summary>
        /// ������� ����� �� ������� �������
        /// </summary>
        public event ClickEventHandler Clicked;

        /// <summary>
        /// ������� ��������� � ������ ��������� �� ������
        /// </summary>
        public event FocusEventHandler OnFocusEventHandler;


        //��� ��������� �� ������ �����, ���������� ������ �����
        //��� ��������� �� �����, ������ �������������� ������ ��� ���
        //��� ��������� �� ������ - �������������� ���� ������
        public abstract void OnPointerEnter(PointerEventData eventData);

        //���������� ������ OnPointerEnter(), �� ����������� ����� ����� ���������
        //��������� �� ������, �������������� ����� ������� ��������� � ������
        public abstract void OnPointerExit(PointerEventData eventData);

        public abstract IEnumerator Move(BaseClickComponent cell);

        //��� ������� ������ �� �������, ���������� ������ �����
        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke(this);
        }

        //���� ����� ����� ������� � �������� ������� (���� ��� ����) � ��� ����� ���������� �����
        //������� �� ��������� ������ � ������������
        protected void CallBackEvent(CellComponent target, bool isSelect)
        {
            OnFocusEventHandler?.Invoke(target, isSelect);
        }
    }

    public enum ColorType
    {
        White,
        Black
    }

    public delegate void ClickEventHandler(BaseClickComponent component);
    public delegate void FocusEventHandler(CellComponent component, bool isSelect);

    public readonly struct Coordinate
    {
        public readonly int X;
        public readonly int Y;

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }
}
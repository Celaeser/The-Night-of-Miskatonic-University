/*
 *	凉屋游戏
 *	ChillyRoom
 *	
 *	Copyright © 2015-2020
 *	
 *  作者：王宏超
 *  时间：
 *  备注：
 *
 */


using UnityEngine;
using UnityEngine.UI;

namespace Ive
{
    public abstract class IveBehaviour : UnityEngine.MonoBehaviour
    {
        private Transform _mTransform = null;
        public Transform mTransform { get { return _mTransform ?? (_mTransform = this.transform); } }

        private BoxCollider _mBoxCollider = null;
        public BoxCollider mBoxCollider { get { return _mBoxCollider ?? (_mBoxCollider = this.GetComponent<BoxCollider>()); } }

        private Rigidbody _mRigidbody = null;
        public Rigidbody mRigidbody { get { return _mRigidbody ?? (_mRigidbody = this.GetComponent<Rigidbody>()); } }

        private Mesh _mMesh = null;
        public Mesh mMesh { get { return _mMesh ?? (_mMesh = this.GetComponent<MeshFilter>().mesh); } }

        private MeshRenderer _mMeshRenderer = null;
        public MeshRenderer mMeshRenderer { get { return _mMeshRenderer ?? (_mMeshRenderer = this.GetComponent<MeshRenderer>()); } }

        private ParticleSystem _mParticleSystem = null;
        public ParticleSystem mParticleSystem { get { return _mParticleSystem ?? (_mParticleSystem = this.GetComponent<ParticleSystem>()); } }

        private ParticleSystemRenderer _mParticleSystemRenderer = null;
        public ParticleSystemRenderer mParticleSystemRenderer { get { return _mParticleSystemRenderer ?? (_mParticleSystemRenderer = this.GetComponent<ParticleSystemRenderer>()); } }

        private ConstantForce _mConstantForce = null;
        public ConstantForce mConstantForce { get { return _mConstantForce ?? (_mConstantForce = this.GetComponent<ConstantForce>()); } }


        private ConstantForce2D _mConstantForce2D = null;
        public ConstantForce2D mConstantForce2D { get { return _mConstantForce2D ?? (_mConstantForce2D = this.GetComponent<ConstantForce2D>()); } }

        private Rigidbody2D _mRigidbody2D = null;
        public Rigidbody2D mRigidbody2D { get { return _mRigidbody2D ?? (_mRigidbody2D = this.GetComponent<Rigidbody2D>()); } }

        private BoxCollider2D _mBoxCollider2D = null;
        public BoxCollider2D mBoxCollider2D { get { return _mBoxCollider2D ?? (_mBoxCollider2D = this.GetComponent<BoxCollider2D>()); } }

        private BoxCollider2D _mChildBoxCollider2D = null;
        public BoxCollider2D mChildBoxCollider2D { get { return _mChildBoxCollider2D ?? (_mChildBoxCollider2D = this.GetComponentInChildren<BoxCollider2D>()); } }

        private CircleCollider2D _mCircleCollider2D = null;
        public CircleCollider2D mCircleCollider2D { get { return _mCircleCollider2D ?? (_mCircleCollider2D = this.GetComponent<CircleCollider2D>()); } }

        private CircleCollider2D _mChildCircleCollider2D = null;
        public CircleCollider2D mChildCircleCollider2D { get { return _mChildCircleCollider2D ?? (_mChildCircleCollider2D = this.GetComponentInChildren<CircleCollider2D>()); } }

        private Canvas _mCanvas = null;
        public Canvas mCanvas { get { return _mCanvas ?? (_mCanvas = this.GetComponent<Canvas>()); } }

        private CanvasScaler _mCanvasScaler = null;
        public CanvasScaler mCanvasScaler { get { return _mCanvasScaler ?? (_mCanvasScaler = this.GetComponent<CanvasScaler>()); } }

        private RectTransform _mRectTransform = null;
        public RectTransform mRectTransform { get { return _mRectTransform ?? (_mRectTransform = this.GetComponent<RectTransform>()); } }

        private Image _mImage = null;
        public Image mImage { get { return _mImage ?? (_mImage = this.GetComponent<Image>()); } }

        private Text _mText = null;
        public Text mText { get { return _mText ?? (_mText = this.GetComponent<Text>()); } }

        private Button _mButton = null;
        public Button mButton { get { return _mButton ?? (_mButton = this.GetComponent<Button>()); } }

        private Outline _mOutline = null;
        public Outline mOutline { get { return _mOutline ?? (_mOutline = this.GetComponent<Outline>()); } }

        private Shadow _mShadow = null;
        public Shadow mShadow { get { return _mShadow ?? (_mShadow = this.GetComponent<Shadow>()); } }



        private Animator _mAnimator = null;
        public Animator mAnimator { get { return _mAnimator ?? (_mAnimator = this.GetComponent<Animator>()); } }

        private AudioSource _mAudioSource = null;
        public AudioSource mAudioSource { get { return _mAudioSource ?? (_mAudioSource = this.GetComponent<AudioSource>()); } }

        private Camera _mCamera = null;
        public Camera mCamera { get { return _mCamera ?? (_mCamera = this.GetComponent<Camera>()); } }

        private SpriteRenderer _mSpriteRenderer = null;
        public SpriteRenderer mSpriteRenderer { get { return _mSpriteRenderer ?? (_mSpriteRenderer = this.GetComponent<SpriteRenderer>()); } }

        private LineRenderer _mLineRenderer = null;
        public LineRenderer mLineRenderer { get { return _mLineRenderer ?? (_mLineRenderer = this.GetComponent<LineRenderer>()); } }
    
    }
}
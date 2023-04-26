using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;
using UnityEngine.AI;
using RPG.Movement;
using UnityEngine.Timeline;

namespace RPG.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject playerPref;
        private Mover _mover;
        private Camera _camera;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _camera = Camera.main;
        }

        private void Update()
        {
            if(InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            var hits = Physics.RaycastAll(GetMouseRay());
            foreach (var hit in hits)
            {
                var target = hit.collider.GetComponent<CombatTarget>();
                if (!target) continue;
                if(Input.GetMouseButtonDown(0))
                    GetComponent<Fighter>().Attack(target);
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            var hasHit = Physics.Raycast(GetMouseRay(), out var hit);
            if (!hasHit) return false;
            if (Input.GetMouseButton(0))
                _mover.MoveTo(hit.point);
            return true;
        }

        private Ray GetMouseRay()
        {
            return _camera.ScreenPointToRay(Input.mousePosition);
        }
    }
}
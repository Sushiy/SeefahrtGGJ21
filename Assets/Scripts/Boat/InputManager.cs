using System;
using UnityEngine;

namespace UnityTemplateProjects.Boat
{
    public class InputManager : MonoBehaviour
    {
        private BoatManager m_boatManager;
        private OrbitalCamera m_camera;
        private QuestSubsystem m_quest;

        public bool m_areAxisInverted;

        private void Start()
        {
            m_boatManager = FindObjectOfType<BoatManager>();
            m_camera = FindObjectOfType<OrbitalCamera>();
            m_quest = FindObjectOfType<QuestSubsystem>();
        }

        private void Update()
        {
            int sign = m_areAxisInverted ? -1 : 1;
            float keyboardY = Input.GetAxis("SpeedUp") * sign;
            float keyboardX = Input.GetAxis("Turn") * sign;
            
            if (keyboardY != 0 || keyboardX != 0)
            {
                if (FastForwardManager.isActive) return;
                m_boatManager.m_boatController.AddSpeed(keyboardY);
                m_boatManager.m_boatController.AddTurnSpeed(keyboardX);
                m_boatManager.TurnAxis = keyboardX;
            }
            float controllerY = Input.GetAxis("ControllerSpeedUp") * sign;
            float controllerX = Input.GetAxis("ControllerTurn") * sign;
            
            if (controllerX != 0 || controllerY != 0)
            {
                if (FastForwardManager.isActive) return;
                m_boatManager.m_boatController.AddSpeed(controllerY);
                m_boatManager.m_boatController.AddTurnSpeed(controllerX);
                m_boatManager.TurnAxis = controllerX;
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                m_camera.m_shouldRotate = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                m_camera.m_shouldRotate = false;
            }
            
            // Camera stuff, 
            Vector2 input = new Vector2(
                Input.GetAxis("MouseY") * sign, Input.GetAxis("MouseX") * sign);
            float magnitudeM = input.sqrMagnitude;
            Vector2 controllerInput =
                new Vector2(Input.GetAxis("VertControllerCam") * sign, Input.GetAxis("HorControllerCam") * sign);
            float magnitudeC = controllerInput.sqrMagnitude;
            
            if(magnitudeC < magnitudeM)
            {
                m_camera.RotateCamera(input, magnitudeM, false);
            }
            else
            {
                m_camera.RotateCamera(controllerInput, magnitudeC, true);
            }

            //UI stuff

            if (Input.GetButtonDown("Submit"))
            {
                m_quest.SkipTyping();
            }
        }
    }
}
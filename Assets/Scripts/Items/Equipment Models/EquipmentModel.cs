using UnityEngine;

namespace ART
{
    [CreateAssetMenu(menuName = "Equipment Model")]
    public class EquipmentModel : ScriptableObject
    {
        public EquipmentModelType equipmentModelType;
        public string maleEquipmentName;
        public string femaleEquipmentName;
        // NAME, MALE
        // NAME, FEMALE

        public void LoadModel(PlayerManager player, bool isMale)
        {
            if (isMale)
            {
                LoadMaleModel(player);
            }
            else
            {
                LoadFemaleModel(player);
            }
        }

        public void LoadMaleModel(PlayerManager player)
        {
            switch (equipmentModelType)
            {
                case EquipmentModelType.FullHelmet:
                    foreach (var model in player.playerEquipmentManager.maleHeadFullHelmets)
                    {
                        Debug.Log(model.gameObject.name);
                        Debug.Log(maleEquipmentName);
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);   
                            Debug.Log("Equip Helmet");
                        }
                    }
                    break;

                case EquipmentModelType.Hat:
                    foreach (var model in player.playerEquipmentManager.hats)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.Hood:
                    foreach (var model in player.playerEquipmentManager.hoods)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.HelmetAcessorie:
                    foreach (var model in player.playerEquipmentManager.helmetAccessories)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.FaceCover:
                    foreach (var model in player.playerEquipmentManager.faceCovers)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.Torso:
                    foreach (var model in player.playerEquipmentManager.maleBodies)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.Back:
                    foreach (var model in player.playerEquipmentManager.backAccessories)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.RightShoulder:
                    foreach (var model in player.playerEquipmentManager.rightShoulders)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.RightUpperArm:
                    foreach (var model in player.playerEquipmentManager.maleRightUpperArms)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.RightElbow:
                    foreach (var model in player.playerEquipmentManager.rightElbows)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.RightLowerArm:
                    foreach (var model in player.playerEquipmentManager.maleRightLowerArms)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.RightHand:
                    foreach (var model in player.playerEquipmentManager.maleRightHands)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.LeftShoulder:
                    foreach (var model in player.playerEquipmentManager.leftShoulders)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.LeftUpperArm:
                    foreach (var model in player.playerEquipmentManager.maleLeftUpperArms)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.LeftElbow:
                    foreach (var model in player.playerEquipmentManager.leftElbows)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.LeftLowerArm:
                    foreach (var model in player.playerEquipmentManager.maleLeftLowerArms)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.LeftHand:
                    foreach (var model in player.playerEquipmentManager.maleLeftHands)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.Hips:
                    foreach (var model in player.playerEquipmentManager.maleHips)
                    {
                        Debug.Log(model.gameObject.name);
                        Debug.Log(maleEquipmentName);
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.HipsAttachment:
                    foreach (var model in player.playerEquipmentManager.hipAccessories)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.RightLeg:
                    foreach (var model in player.playerEquipmentManager.maleRightLegs)
                    {
                        //Debug.Log("RightLeg");
                        //Debug.Log(model.gameObject.name);
                        //Debug.Log(maleEquipmentName);
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.RightKnee:
                    foreach (var model in player.playerEquipmentManager.rightKnees)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.LeftLeg:
                    foreach (var model in player.playerEquipmentManager.maleLeftLegs)
                    {
                        //Debug.Log("LeftLeg");
                        //Debug.Log(model.gameObject.name);
                        //Debug.Log(maleEquipmentName);
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.LeftKnee:
                    foreach (var model in player.playerEquipmentManager.leftKnees)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
            }
        }

        public void LoadFemaleModel(PlayerManager player)
        {
            switch (equipmentModelType)
            {
                case EquipmentModelType.FullHelmet:
                    foreach (var model in player.playerEquipmentManager.femaleHeadFullHelmets)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.Hat:
                    foreach (var model in player.playerEquipmentManager.hats)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.Hood:
                    foreach (var model in player.playerEquipmentManager.hoods)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.HelmetAcessorie:
                    foreach (var model in player.playerEquipmentManager.helmetAccessories)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.FaceCover:
                    foreach (var model in player.playerEquipmentManager.faceCovers)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.Torso:
                    foreach (var model in player.playerEquipmentManager.femaleBodies)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.Back:
                    foreach (var model in player.playerEquipmentManager.backAccessories)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.RightShoulder:
                    foreach (var model in player.playerEquipmentManager.rightShoulders)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.RightUpperArm:
                    foreach (var model in player.playerEquipmentManager.femaleRightUpperArms)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.RightElbow:
                    foreach (var model in player.playerEquipmentManager.rightElbows)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.RightLowerArm:
                    foreach (var model in player.playerEquipmentManager.femaleRightLowerArms)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.RightHand:
                    foreach (var model in player.playerEquipmentManager.femaleRightHands)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.LeftShoulder:
                    foreach (var model in player.playerEquipmentManager.leftShoulders)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.LeftUpperArm:
                    foreach (var model in player.playerEquipmentManager.femaleLeftUpperArms)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.LeftElbow:
                    foreach (var model in player.playerEquipmentManager.leftElbows)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.LeftLowerArm:
                    foreach (var model in player.playerEquipmentManager.femaleLeftLowerArms)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.LeftHand:
                    foreach (var model in player.playerEquipmentManager.femaleLeftHands)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.Hips:
                    foreach (var model in player.playerEquipmentManager.hipAccessories)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.HipsAttachment:
                    foreach (var model in player.playerEquipmentManager.femaleHips)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.RightLeg:
                    foreach (var model in player.playerEquipmentManager.femaleRightLegs)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.RightKnee:
                    foreach (var model in player.playerEquipmentManager.rightKnees)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.LeftLeg:
                    foreach (var model in player.playerEquipmentManager.femaleLeftLegs)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.LeftKnee:
                    foreach (var model in player.playerEquipmentManager.leftKnees)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
            }
        }
    }
}
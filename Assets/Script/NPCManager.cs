using ChangeLab.ArtGallery.Player;
using UnityEngine;
namespace ChangeLab.ArtGallery
{
    public class NPCManager
    {
        private NPCController[] nPCControllers;

        public NPCManager(NPCController[] nPCControllers, Transform playerTransfrom)
        {
            this.nPCControllers = nPCControllers;
            foreach (var npc in nPCControllers)
            {
                npc.player = playerTransfrom;
            }
        }


        public bool GetNearestNPC(Transform player, out NPCController nPCController)
        {
            nPCController = null;
            float min = Mathf.Infinity;
            foreach (var npc in nPCControllers)
            {
                if (Utils.IsInRange(npc.poistionObj, player, npc.range))
                {
                    float distance = Vector3.Distance(player.position, npc.transform.position);
                    if (distance <= 1 && distance < min)
                    {
                        min = distance;
                        nPCController = npc;
                    }
                }
            }
            return nPCController != null;
        }
    }

}

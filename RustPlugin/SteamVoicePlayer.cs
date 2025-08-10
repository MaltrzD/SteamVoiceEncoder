using Network;
using Oxide.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Oxide.Plugins
{
    [Info("SteamVoicePlayer", "MaltrzD", "0.1")]
    class SteamVoicePlayer : RustPlugin
    {
        [ChatCommand("play")]
        private void LoadClip(BasePlayer p)
        {
            var sound = Interface.Oxide.DataFileSystem.ReadObject<SteamVoiceSound>("SteamVoicePlayer/Sounds/sound");

            playCoroutine = ServerMgr.Instance.StartCoroutine(PlayAudio(sound));
        }
        Coroutine playCoroutine = null;


        private BasePlayer bot;
        private void Speak(byte[] data, Vector3 pos)
        {
            if (bot == null)
            {
                bot = GameManager.server.CreateEntity("assets/prefabs/player/player.prefab", pos) as BasePlayer;
                bot.Spawn();
            }

            //Server.Broadcast($"speak: {data.Length}");

            NetWrite netWrite = Network.Net.sv.StartWrite();
            netWrite.PacketID(Message.Type.VoiceData);
            netWrite.EntityID(bot.net.ID);
            netWrite.BytesWithSize(data);

            netWrite.Send(new SendInfo(new List<Connection>() { BasePlayer.activePlayerList[0].Connection })
            {
                priority = Priority.Immediate
            });
        }
        private IEnumerator PlayAudio(SteamVoiceSound sound)
        {
            if (sound.Data == null || sound.Data.Count == 0 || sound.SecondsDuration <= 0)
                yield break;

            var player = BasePlayer.activePlayerList[0];

            int packetCount = sound.Data.Count;
            double totalDurationSeconds = sound.SecondsDuration;

            var startTimeTicks = DateTime.Now.Ticks;

            for (int i = 0; i < packetCount; i++)
            {
                double progress = (double)i / packetCount;
                long targetTicks = startTimeTicks + (long)(progress * totalDurationSeconds * 10_000_000L);

                while (DateTime.Now.Ticks < targetTicks)
                    yield return null;

                Speak(sound.Data[i], player.transform.position);
            }
        }

        public class SteamVoiceSound
        {
            public double SecondsDuration = 0;
            public List<byte[]> Data = new List<byte[]>();
        }
    }
}
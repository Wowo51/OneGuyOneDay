using System;
using System.Collections.Generic;
using System.Linq;

namespace NaglesAlgorithm
{
    public class NagleAlgorithmEngine
    {
        private readonly List<string> _packetBuffer;
        private readonly int _maxBufferSize;
        private readonly INetworkSender _networkSender;
        public NagleAlgorithmEngine(int maxBufferSize, INetworkSender networkSender)
        {
            _maxBufferSize = maxBufferSize > 0 ? maxBufferSize : 1024;
            _networkSender = networkSender ?? new DefaultNetworkSender();
            _packetBuffer = new List<string>();
        }

        public void QueuePacket(string packet)
        {
            if (string.IsNullOrEmpty(packet))
            {
                return;
            }

            _packetBuffer.Add(packet);
            int currentBufferLength = 0;
            foreach (string bufferedPacket in _packetBuffer)
            {
                currentBufferLength += bufferedPacket.Length;
            }

            if (currentBufferLength >= _maxBufferSize)
            {
                FlushBuffer();
            }
        }

        public void FlushBuffer()
        {
            if (_packetBuffer.Count > 0)
            {
                string coalescedPacket = string.Join("", _packetBuffer);
                _networkSender.Send(coalescedPacket);
                _packetBuffer.Clear();
            }
        }
    }

    public class DefaultNetworkSender : INetworkSender
    {
        public void Send(string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                Console.WriteLine("Sending coalesced packet: " + data);
            }
        }
    }
}
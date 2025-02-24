using System.Collections.Generic;

namespace PaxosAlgorithm
{
    public class PaxosProposer
    {
        private int _proposalNumber;
        private string _value;
        private List<PaxosAcceptor> _acceptors;
        public PaxosProposer(int proposalNumber, string value, List<PaxosAcceptor> acceptors)
        {
            _proposalNumber = proposalNumber;
            _value = value;
            _acceptors = acceptors;
        }

        public bool RunProposal()
        {
            int promiseCount = 0;
            foreach (PaxosAcceptor acceptor in _acceptors)
            {
                bool promise = acceptor.ReceivePrepare(_proposalNumber);
                if (promise)
                {
                    promiseCount++;
                }
            }

            if (promiseCount <= _acceptors.Count / 2)
            {
                return false;
            }

            int acceptCount = 0;
            foreach (PaxosAcceptor acceptor in _acceptors)
            {
                bool accepted = acceptor.ReceiveAccept(_proposalNumber, _value);
                if (accepted)
                {
                    acceptCount++;
                }
            }

            return (acceptCount > _acceptors.Count / 2);
        }
    }
}
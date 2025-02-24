namespace PaxosAlgorithm
{
    public class PaxosAcceptor
    {
        private int _promisedProposal;
        private int _acceptedProposal;
        private string _acceptedValue;
        public PaxosAcceptor()
        {
            _promisedProposal = 0;
            _acceptedProposal = 0;
            _acceptedValue = "";
        }

        public bool ReceivePrepare(int proposalNumber)
        {
            if (proposalNumber > _promisedProposal)
            {
                _promisedProposal = proposalNumber;
                return true;
            }

            return false;
        }

        public bool ReceiveAccept(int proposalNumber, string value)
        {
            if (proposalNumber >= _promisedProposal)
            {
                _promisedProposal = proposalNumber;
                _acceptedProposal = proposalNumber;
                _acceptedValue = value;
                return true;
            }

            return false;
        }

        public int GetAcceptedProposal()
        {
            return _acceptedProposal;
        }

        public string GetAcceptedValue()
        {
            return _acceptedValue;
        }
    }
}
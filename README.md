#OneGuyOneDay

This repo contains 433 algorithms. The algorithms were written in one day by me and my Raven code generation system.

Pure C# libraries. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Raven can go from a prompt to a unit test report stating that the app has passed testing in one click. As LLM's typically make a few mistakes when writing code, working out all of the bugs and unit testing the code is crucial. It can write algorithms in parallel. This run used a setting of writing 10 algorithms simultaneously. An 8-core pc was used. My tier 5 OpenAI account could handle many more simultaneous API calls than my 8 core.

Raven does not always complete an algorithm with a single pass of the code generation tool. Only a single pass was used. 433 out of 609 of the algorithms were solved.

Total solve time was under 15.5 hours. Human labor cost would be about a day's work as Raven ran unattended for part of the day. The OpenAI API was used by Raven. API cost was less than $168.

At $50/hr total cost with me and Raven: $568.

According to ChatGPT when asked for a quote:
Hand-Written Code: Approximately $110,000–$130,000
ChatGPT-Assisted Development: Approximately $55,000–$65,000 (Copying and pasting from ChatGPT).

So can I actually write code 100 times cheaper than someone copying and pasting from ChatGPT? No actually. Raven can solve these types of simple algorithms very efficiently. Writing a complex integrated app is not the same thing and costs will be higher. I can no doubt write code cheaper than someone copying and pasting from ChatGPT in many cases.

Here's a list of the algorithms:
AC3Algorithm,AdaBoostAdaptiveBoosting,AdaptiveHuffmanCoding,AdaptiveReplacementCache,Adler32,AdvancedEncryptionStandard,AKSPrimalityTest,AlgorithmX,AlmeidaPinedaBackpropagation,AlphaBetaAlgorithm,AlphaBetaPruning,AntColonyOptimization,ApproximateCountingAlgorithm,AprioriAlgorithm,AriesRecovery,ArnoldiIteration,AssociationRuleLearning,AStarSearch,AverageLinkageClustering,BabyGiantStep,BacktrackingSearch,BaileyBorweinPlouffe,BailliePSWTest,BankersAlgorithm,BcjrAlgorithm,Bcrypt,BeadSort,BeamSearch,BeamStackSearch,BeesAlgorithm,BellmanFordAlgorithm,BensonAlgorithm,BerlekampMasseyAlgorithm,BestBinFirst,BfgsMethod,BiconjugateGradientMethod,BidirectionalSearch,BinaryGCD,BinarySearchAlgorithm,BinarySplitting,BinaryTreeSort,BisectionMethod,BitonicSorter,BlakeysSecretSharing,BlockLoopJoin,BlockTruncationCoding,Bogosort,BoothsMultiplicationAlgorithm,BootstrapAggregating,BoruvkasAlgorithm,BorweinAlgorithm,BoyerMooreAlgorithm,BoyerMooreHorspool,BreadthFirstSearch,BronKerboschAlgorithm,BruunsFftAlgorithm,BubbleSort,BuchbergerAlgorithm,BuddyMemoryAllocation,BullyAlgorithm,BurstSortTrie,BuzenAlgorithm,BytePairEncoding,C3Linearization,C45Algorithm,CannonsAlgorithm,CannyEdgeDetector,CanonicalLRParser,CanopyClusteringAlgorithm,CantorZassenhausAlgorithm,ChaffAlgorithm,ChainMatrixMultiplication,ChaitinRegisterAllocation,ChandraTouegConsensus,ChandyLamportAlgorithm,ChaseDependency,ChienSearch,ChristofidesAlgorithm,ChsConversion,ChudnovskyAlgorithm,CipollaAlgorithm,ClockAdaptiveReplacement,CocktailShakerSort,CombSort,CompleteLinkageClustering,ConjugateGradient,ConjugateGradientMethods,ConnectedComponentLabeling,ContextTreeWeighting,CooleyTukeyFFT,CoppersmithWinogradAlgorithm,CristianAlgorithmSync,CrossEntropyMethod,CuthillMcKeeAlgorithm,CuttingPlaneMethod,CycleSortInPlace,CykAlgorithm,DaitchMokotoffSoundex,DamerauLevenshteinDistance,DancingLinks,DataEncryptionStandard,DbscanClustering,Deflate,DeltaEncoding,DepthFirstSearch,DeutschJozsaAlgorithm,DiceCoefficient,DifferenceMapAlgorithm,DifferentialEvolution,DiffieHellmanExchange,DijkstraScholtenAlgorithm,DinicsAlgorithm,DiscreteFourierTransform,DixonAlgorithm,DoomsdayAlgorithm,DoubleDabble,DynamicTimeWarping,EarleyParserAlgorithm,EarliestDeadlineFirst,ElevatorAlgorithm,EliasDeltaCoding,EllipsoidMethod,EllipticCurveCryptography,EllipticCurveDH,ElserDifferenceMap,EmbeddedZerotreeWavelet,EscHeartFailure,EspressoHeuristicMinimizer,EuclideanAlgorithm,EulerIntegration,EvolutionStrategy,ExponentialBackoff,ExponentialGolombCoding,ExponentiatingBySquaring,FalsePositionMethod,FastCosineTransform,FastFoldingAlgorithm,FastFourierTransform,FaugereF4Algorithm,FELICSImageCompression,FermatsFactorizationMethod,FibonacciCoding,FibonacciSearchTechnique,FiniteDifferenceMethod,FisherYatesShuffle,FitnessProportionateSelection,FlameClustering,FletchersChecksum,FnvHash,FordFulkersonAlgorithm,FrankWolfeAlgorithm,FreivaldsAlgorithm,FurerAlgorithm,FuzzyClustering,FuzzyCMeans,GaussianElimination,GaussJordanElimination,GaussSeidelMethod,GeneExpressionProgramming,GeneralisedHoughTransform,GeneralProblemSolver,GenerationalGarbageCollector,GeneticAlgorithm,GeohashAlgorithm,GerchbergSaxtonAlgorithm,GibbsSampling,GirvanNewmanAlgorithm,GlauberDynamics,GnomeSort,GoertzelAlgorithm,GoldschmidtDivision,GolombCodingOptimal,GrabcutGraphCuts,GradientDescent,GramSchmidtProcess,GraspAlgorithm,GridSearchOptimization,HammingDistance,HammingWeight,HashFunction,HashJoin,HeapSort,HindleyMilnerAlgorithm,HirschbergsAlgorithm,HistogramEqualization,HmacMessageAuthentication,HopcroftMooreBrzozowski,HopfieldNet,HoughTransform,HuangAlgorithm,HuffmanCoding,HybridBfgsMethod,HybridGradientAlgorithm,HybridMonteCarlo,HyperlinkInducedSearch,ID3Algorithm,IncrementalDeltaEncoding,InsertionSort,InsideOutsideAlgorithm,IntersectionClockSynchronization,InverseIteration,IterativeDeepeningSearch,ItpMethod,JacobiMethod,JumpSearch,KadaneAlgorithm,KalmanFilter,KaratsubaAlgorithm,KargersAlgorithm,KarnsAlgorithm,KarplusStrongSynthesis,KhopcaClusteringAlgorithm,KMeansPlusPlus,KMedoids,KnuthMorrisPratt,KosarajuAlgorithm,KraussWildcardAlgorithm,KruskalAlgorithm,KthLargest,KWayMergeAlgorithm,LamportsBakeryAlgorithm,LanczosIteration,LaxWendroffMethod,LeastSlackTime,LempelZivBonwick,LempelZivMarkov,LempelZivWelch,LempelZivWilliams,LenstraEllipticFactorization,LeskAlgorithm,LevenbergMarquardtAlgorithm,LevenshteinCoding,LexBfs,LindeBuzoGray,LinearFeedbackRegister,ListScheduling,LLLAlgorithm,LloydsAlgorithm,LLParser,LocalitySensitiveHashing,LocalSearch,LogitBoost,LongDivisionAlgorithm,LongestCommonSubsequence,LongestCommonSubstring,LongestIncreasingSubsequence,LongitudinalRedundancyCheck,LossyNormalMap,LPBoost,LRParser,LuhnAlgorithm,LuhnModN,Lz77AndLz78,Lzss,LZWLSyllableVariant,Mae1,MarkAndSweep,MatchRatingApproach,MaximumParsimony,Md5Collisions,MedianFiltering,MergeSort,MetropolisHastingsAlgorithm,MillerRabinTest,MinimaxAlgorithm,MinimumDegreeAlgorithm,MiserAlgorithm,MuLawAlgorithm,MullersMethod,MultivariateDivisionAlgorithm,NaglesAlgorithm,NearestNeighborSearch,NeedlemanWunsch,NestedLoopJoin,NestedSamplingAlgorithm,NewtonMethod,NewtonRaphsonDivision,NewtonsMethod,NonRestoringDivision,NthRootAlgorithm,NTRUEncrypt,OddsAlgorithm,OddsBrussAlgorithm,OdlyzkoSchonhageAlgorithm,OneAttributeRule,OperatorPrecedenceParser,OpticsClusteringAlgorithm,OrderedDithering,PackageMergeAlgorithm,PackratParser,PancakeSorting,ParityErrorDetection,PartialLeastSquares,ParticleSwarmOptimization,PatienceSorting,PaxosAlgorithm,Pbkdf2,PearsonHash,Perceptron,PetersonAlgorithm,PetricksMethod,PigeonholeSort,PohligHellmanAlgorithm,PostmanSort,PowerIteration,PowersetConstruction,PrattParser,PredictiveSearch,PrimsAlgorithm,PulseNeuralNetworks,PushRelabelAlgorithm,QLearningAlgorithm,QrAlgorithmEigenvalues,QuadraticSieve,QuickSortDivide,QuineMcCluskeyAlgorithm,RadialBasisNetwork,RadixSort,RaftAlgorithm,RandomForest,RandomHillClimb,RandomSampleConsensus,RandomSearch,RayleighQuotientIteration,Rc4Cipher,RecursiveDescentParser,RegionGrowing,RelevanceVectorMachine,RestoringDivisionAlgorithm,ReteAlgorithm,RichardsonLucyDeconvolution,RichSalzWildmat,RidderMethod,RiemersmaDithering,Ripemd160,Rsa,RsaDigitalSignature,RunLengthEncoding,RuzzoTompaAlgorithm,Salsa20AndChaCha20,SampleSort,SchenstedAlgorithm,SchonhageStrassenAlgorithm,SchreierSimsAlgorithm,ScoringAlgorithm,SeamCarvingAlgorithm,SecantMethod,SelectionSort,SelfOrganizingMap,SemiSpaceCollector,SequiturAlgorithm,Sha1Collisions,Sha2Variants,Sha3Variants,ShamirsSecretSharing,ShannonFanoCoding,ShannonFanoElias,ShellSort,ShortestCommonSupersequence,ShortestJobScheduling,ShortestRemainingTime,ShortestSeekFirst,ShuntingYardAlgorithm,SieveEratosthenes,SieveOfSundaram,SimonsAlgorithm,SimpleLrParser,SimplePrecedenceParser,SimplexAlgorithm,SimulatedAnnealing,SingleLinkageClustering,SipHash,SlowSort,SmithWatermanAlgorithm,SmoothGamerSort,SnapshotAlgorithm,SortingSignedReversals,SortMergeJoin,Spiht,StateSpaceSearch,SteinhausJohnsonTrotter,StochasticTunnelingAlgorithm,StochasticUniversalSampling,StoneMethodSIP,StoogeSort,StrassenAlgorithm,StringMetrics,StructuredSvmPredictor,SubcluAlgorithm,SubsetSumAlgorithm,SuccessiveOverRelaxation,SukhotinsAlgorithm,SupportVectorMachine,SurfFeatureDetector,SymbolicCholeskyDecomposition,TabuSearchAlgorithm,TarjanSCC,TemporalDifferenceLearning,TernarySearch,Threefish,TonelliShanksAlgorithm,ToomCookMultiplication,TopNodesAlgorithm,TopologicalSort,TridiagonalMatrixAlgorithm,TrigramSearch,TruncatedBinaryEncoding,TruncatedExponentialBackoff,TruncationSelection,UnaryCoding,UnicodeCollationAlgorithm,UniformBinarySearch,UniformCostSearch,UnionMergeUnique,UpgmaAlgorithm,VectorQuantization,VelvetAlgorithmSuite,VerhoeffAlgorithm,VerletIntegration,VincentysFormulae,ViterbiAlgorithm,WACAClusteringAlgorithm,WardsMethod,WarnsdorffsRule,WarpedPredictiveCoding,WatershedTransformation,Whirlpool,WinnowAlgorithm,XorSwapAlgorithm,YarrowAlgorithm,ZellersCongruence,ZeroAttributeRule,ZobristHashing

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>

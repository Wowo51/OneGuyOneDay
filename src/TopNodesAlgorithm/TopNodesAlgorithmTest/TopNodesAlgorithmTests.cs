using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TopNodesAlgorithm;
using TNA = TopNodesAlgorithm.TopNodesAlgorithm;

namespace TopNodesAlgorithmTest
{
    [TestClass]
    public class TopNodesAlgorithmTests
    {
        [TestMethod]
        public void GetTopNodes_NullInput_ReturnsEmptyList()
        {
            List<ResourceCalendarEvent>? events = null;
            List<ResourceCalendarEvent> result = TNA.GetTopNodes(events);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetTopNodes_EmptyInput_ReturnsEmptyList()
        {
            List<ResourceCalendarEvent> events = new List<ResourceCalendarEvent>();
            List<ResourceCalendarEvent> result = TNA.GetTopNodes(events);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetTopNodes_SingleEvent_ReturnsSameEvent()
        {
            DateTime start = new DateTime(2000, 1, 1, 8, 0, 0);
            DateTime end = new DateTime(2000, 1, 1, 17, 0, 0);
            ResourceCalendarEvent singleEvent = new ResourceCalendarEvent(start, end, "Single Event");
            List<ResourceCalendarEvent> events = new List<ResourceCalendarEvent>
            {
                singleEvent
            };
            List<ResourceCalendarEvent> result = TNA.GetTopNodes(events);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(singleEvent, result[0]);
        }

        [TestMethod]
        public void GetTopNodes_EventContained_RemovesContainedEvent()
        {
            DateTime parentStart = new DateTime(2000, 1, 1, 8, 0, 0);
            DateTime parentEnd = new DateTime(2000, 1, 1, 17, 0, 0);
            ResourceCalendarEvent parentEvent = new ResourceCalendarEvent(parentStart, parentEnd, "Parent");
            DateTime childStart = new DateTime(2000, 1, 1, 9, 0, 0);
            DateTime childEnd = new DateTime(2000, 1, 1, 10, 0, 0);
            ResourceCalendarEvent childEvent = new ResourceCalendarEvent(childStart, childEnd, "Child");
            List<ResourceCalendarEvent> events = new List<ResourceCalendarEvent>
            {
                parentEvent,
                childEvent
            };
            List<ResourceCalendarEvent> result = TNA.GetTopNodes(events);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(parentEvent, result[0]);
        }

        [TestMethod]
        public void GetTopNodes_MultipleIndependentEvents_ReturnsAllEvents()
        {
            ResourceCalendarEvent eventA = new ResourceCalendarEvent(new DateTime(2000, 1, 1, 8, 0, 0), new DateTime(2000, 1, 1, 10, 0, 0), "A");
            ResourceCalendarEvent eventB = new ResourceCalendarEvent(new DateTime(2000, 1, 1, 11, 0, 0), new DateTime(2000, 1, 1, 12, 0, 0), "B");
            List<ResourceCalendarEvent> events = new List<ResourceCalendarEvent>
            {
                eventA,
                eventB
            };
            List<ResourceCalendarEvent> result = TNA.GetTopNodes(events);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            CollectionAssert.Contains(result, eventA);
            CollectionAssert.Contains(result, eventB);
        }

        [TestMethod]
        public void GetTopNodes_EqualEvents_ReturnsEmptyList()
        {
            DateTime start = new DateTime(2000, 1, 1, 8, 0, 0);
            DateTime end = new DateTime(2000, 1, 1, 17, 0, 0);
            ResourceCalendarEvent event1 = new ResourceCalendarEvent(start, end, "Event1");
            ResourceCalendarEvent event2 = new ResourceCalendarEvent(start, end, "Event2");
            List<ResourceCalendarEvent> events = new List<ResourceCalendarEvent>
            {
                event1,
                event2
            };
            List<ResourceCalendarEvent> result = TNA.GetTopNodes(events);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetTopNodes_SyntheticTest_CheckTopNodesProperty()
        {
            List<ResourceCalendarEvent> events = GenerateSyntheticEvents(1000);
            List<ResourceCalendarEvent> topNodes = TNA.GetTopNodes(events);
            foreach (ResourceCalendarEvent topNode in topNodes)
            {
                Boolean contained = false;
                foreach (ResourceCalendarEvent ev in events)
                {
                    if (ev != topNode && ev.Start <= topNode.Start && ev.End >= topNode.End)
                    {
                        contained = true;
                        break;
                    }
                }

                Assert.IsFalse(contained, "Top node is contained by another event.");
            }
        }

        private List<ResourceCalendarEvent> GenerateSyntheticEvents(int count)
        {
            List<ResourceCalendarEvent> events = new List<ResourceCalendarEvent>();
            DateTime baseDate = new DateTime(2000, 1, 1);
            Random random = new Random(0);
            for (int i = 0; i < count; i++)
            {
                int startOffset = random.Next(0, 10000);
                int duration = random.Next(1, 500);
                DateTime start = baseDate.AddMinutes(startOffset);
                DateTime end = start.AddMinutes(duration);
                events.Add(new ResourceCalendarEvent(start, end, "Synthetic " + i));
            }

            return events;
        }
    }
}
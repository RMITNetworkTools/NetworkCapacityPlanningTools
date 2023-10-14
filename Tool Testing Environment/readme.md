TESTING ENVIRONMENT:

A testing environment in GNS3, a network simulation platform, featuring a collection of routers, switches, and hosts has been setup.The purpose of this setup is to facilitate the evaluation and testing of code designed for the Network Capacity Planning Tools project. These tools are integral for the comprehensive analysis, planning, bottleneck identification, and the assessment of routing protocols within the network. Additionally, they serve as a resource for investigating and addressing network issues while offering insights for future network expansion.

The configuration involves a VMware virtual machine (VM) on a host PC, running the GNS3 software. The virtual network is connected to the host PC via a bridged VM to virtual loopback interface on the Host PC runnin. This connection allows the network to seamlessly interact with code executed on the host PC, enabling thorough testing and analysis. Furthermore, the simulated routers have been equipped with their respective ISO images, enabling an accurate representation of real-world network conditions. This environment, controlled via the GNS3 graphical user interface (GUI), offers a more practical approach for in-depth exploration of network capacity planning and performance optimization.

The code base for the capacity planning tools is developed in the C# programming language interfacing with the GNS3 network.

Within this folder, includes:
- An image of the GNS3 GUI, including the testing environment network/topology.
- An image of the GNS3 VM Interface.
- Imagea of the VM network adapter and virtual ethernet interface (bridged).

This testing environment has been used to prove the functionality of the tools within other folders of this repo. 
The generated outputs of some tools have directly utilised this network to obtain network information. (i.e. SNMP Network Scoping Tools)

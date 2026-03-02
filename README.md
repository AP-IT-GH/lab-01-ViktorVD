# Rapport Opdracht ML-Agent

## Inleiding
Dit rapport beschrijft het ontwerpproces en de resultaten van een reinforcement learning experiment in Unity dat gebruik maakt van de ML-Agents toolkit. Het doel van dit onderzoek was het ontwikkelen van een agent die taken kan oplossen, zoals het lokaliseren van een willekeurig geplaatst doelwit om vervolgens succesvol te navigeren naar een eindplatform.

Dit document is bedoeld als een zelfreflectie voor dit labo, maar ook voor ontwikkelaars en docenten die inzicht willen krijgen in de implementatie van de logica, observaties en beloningsfunctie binnen een 3D-simulatie van Unity. Het onderzoek toont aan hoe machine learning modellen complexe navigatietaken kunnen aanleren door middel van trial-and-error.

## Methoden
De oefening is opgebouwd rond twee componenten binnen de Unity-omgeving.

### Behavior Parameters 
Deze bestaat uit 2 delen:
* **Vector Observation Space:** De grootte is ingesteld op 10. De agent observeert zijn eigen 3D-positie (3), de positie van de Target (3), de positie van het Goal-platform (3) en een boolean status die aangeeft of de Target al is bereikt (1).
* **Actions:** Er is gekozen voor 2 continue acties die de X- en Z-as aansturen voor de voortbeweging van de agent.

### De Agent 
De functies van de agent gebruiken een overerving van de `Agent` base class van Unity. Hierbij gebruiken we functies om op onze oefening toe te passen:
* `OnEpisodeBegin()`**:** Deze methode reset de simulatie aan het begin van elke poging. De agent wordt teruggeplaatst naar het nulpunt en de Target wordt op een willekeurige nieuwe plek geplaatst.
* `CollectObservations(VectorSensor sensor)`**:** Deze verzamelt de data voor de agent over de Unity wereld. Zo worden de posities van de agent, Target en Goal doorgegeven, alsook de status van de Target.
* `OnActionReceived(ActionBuffers actionBuffers)`**:** Deze methode vertaalt de output van het neurale netwerk naar fysieke beweging (`transform.Translate`). Tevens worden hier negatieve beloningen uitgedeeld als de agent van het platform valt (`SetReward(-1.0f)`).

## Resultaten
Tijdens de initiële trainingsfase werd geobserveerd dat de agent frequent van het veld viel. Na ongeveer 60.000 stappen begon de agent de oefening juist uit te werken. De agent had in eerste instantie moeite met de volgorde van wat hij moest doen. Na het toevoegen van de *targetbereikt* status, de observaties en visuele feedback ging de agent na ongeveer 100.000 stappen de taak consistent en succesvol voltooien.

## Conclusie
Uit de observaties kunnen we concluderen dat het trainen van een agent afhankelijk is van het correct schrijven van het geheugen van de agent. Doordat de actuele status van *targetbereikt* als observatie werd meegegeven, was de AI in staat zijn doelstelling halverwege de episode succesvol te wijzigen. Het gebruik van Unity's ingebouwde triggers geeft echter ook wat problemen, zoals de speler niet altijd juist detecteren als hij erop loopt. De getrainde agent is nu in staat om efficiënt ruimtelijke problemen op te lossen.

## Referenties
* Unity Technologies. (2026). *ML-Agents Toolkit Documentation*. Geraadpleegd via https://github.com/Unity-Technologies/ml-agents
* AP University College Antwerpen. (z.d.). *Digitap-cursus [Online leeromgeving]*. Geraadpleegd via https://learning.ap.be/course/view.php?id=71804

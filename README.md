# unity-rho-events-package
Use Event Scriptable Object to decouple game systems. Use the provided events or create your own using the provided wizard.

## Installation
In Window->Package Manager, `install package from git URL` and use this repo's address.
![image](https://github.com/user-attachments/assets/2ee39f30-2946-4b99-a345-bd7f0abbdad9)

## Examples

## How to use an Event
1. Create a new Event by right clicking in the Project->Assets folder and selecting Rho->Events->Event.
2. Name your new event Scriptable Object.
3. In a GameObject, add a new Componenent 'EventListener.'
4. In the component, select your Event Asset.
5. In the component, add a response callback. For example, select the object the component is on, then select GameObject->name and type 'TEST EVENT' as the paramter.
6. Run the game.
7. Select your new event in the Inspector and click the 'Invoke' button.
8. Observe how the event was fired. For example, the gameobject will now have its name set to 'TEST EVENT.'

### How to Create new Event Types with an Event Paramter
1. In the Project view, right click on Assets and select Create->Rho->Create New Event<T> Type
2. In the Wizard, type in the type of the data you wish to create a new event type of.
3. If you wish to add an incomplete Editor script to add test parameters to, you can select the Editor toggle button.
4. Click Create

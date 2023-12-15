import { ComponentProps, FormEvent, useRef, useState } from "react"
import Subtask from "./subtask"
import api from "../api"
import { View, Text, FlatList, TouchableOpacity } from "react-native"
import NewSubtask from "./newsubtask"
// import { ListItem } from "react-native-elements"


const Task = (props) => {
    const [task, setTask] = useState(props.task)

    const removeClicked = async () => {
        // TODO ask to confirm
        // const confirmed = window.confirm(`Remove task \'${task.title}\'?`)
        // if (!confirmed) return

        const resp = await api.delete(`/api/tasks/${props.task.id}`)
        props.onTasksUpdated(resp.data)
    }

    
    
    return <View style={{border: "1px solid black", padding: 10}}>
        <Text>{task.title}</Text>
        <Text style={{paddingLeft: 8}}>{task.description}</Text>
        <View>
            <FlatList 
                data={task.subtasks}
                renderItem={({item}) => <Subtask ownerTaskID={task.id} subtask={item} />}
            />
            <NewSubtask onUpdated={newTask => setTask(newTask)} task={props.task} onAdded={t => setTask(t)}/>
            {/* <FlatList>
                {task.subtasks.map(st => (
                    <View key={st.id.toString()}>
                        
                    </View>
                ))}
            </FlatList> */}
        </View>
        <TouchableOpacity onPress={removeClicked}>
            <Text>Remove</Text>
        </TouchableOpacity>
    </View>
}

export default Task
import * as SecureStore from "expo-secure-store";
import { Platform } from "react-native";

const local = {}

// TODO bad, change

const isWeb = () => Platform.OS == 'web'

export const getStored = async (key) => {
    if (isWeb()) {
        return local[key]
    }
    return await SecureStore.getItemAsync(key)   
}

export const setStored = async (key, value) => {
    if (isWeb()) {
        local[key] = value
        return
    }
    return await SecureStore.setItemAsync(key, value)
}

export const unsetStored = async (key) => {
    if (isWeb()) {
        delete local[key]
        return
    }
    return await SecureStore.deleteItemAsync(key)
}